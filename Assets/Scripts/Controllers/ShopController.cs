using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class ShopController : MonoBehaviour
    {
        public Transform shelfTransform;
        public Transform cartTransform;
        public GameObject slotPrefab;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI totalText;
        public Dialog dialog;

        private int total;
        private int money;
        private Canvas _canvas;
        private readonly List<SimpleItem.ItemData> _cart = new();

        public void Start()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            total = 0;
            totalText.text = $"{total}";
            money = int.Parse(moneyText.text);
            dialog.Write("Welcome to the shop! Feel free to browse our selection of items.");
        }

        public void Open()
        {
            _canvas.enabled = true;
            MovementController.CanMove.Add("Shop");
            OpenTab("BodyArmor");
        }

        public void Close()
        {
            _canvas.enabled = false;
            MovementController.CanMove.Remove("Shop");
        }

        /// <summary>
        /// Remove all the items from the parent shelf and load the new ones.
        /// </summary>
        /// <param name="items">The items to load.</param>
        /// <param name="parentTransform">The parent transform of the items.</param>
        /// <param name="onSlotClick">The function to call when a slot is clicked.</param>
        private IEnumerator LoadItems(SimpleItem.ItemData[] items, Transform parentTransform, UnityAction<SimpleItem.ItemData> onSlotClick)
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
    
            // Wait for one frame
            yield return new WaitForEndOfFrame();
    
            foreach (var item in items)
            {
                var slot = Instantiate(slotPrefab, parentTransform);
                // Moves the slot to the right position with an offset of 110 on x and 110 on y every 4 slots instantiated.
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    slot.transform.GetSiblingIndex() % 4 * 110,
                    -110 * (slot.transform.GetSiblingIndex() / 4)
                );
                var slotData = slot.GetComponent<Slot>();
                slotData.item = item;
                slotData.OnSlotClick = onSlotClick;
            }
        
            var parentRectTransform = parentTransform.GetComponent<RectTransform>();
            parentRectTransform.sizeDelta = new Vector2(
                parentRectTransform.sizeDelta.x,
                110 * (parentTransform.childCount / 4 + 1)
            );
        }

        /// <summary>
        /// Search all the items in the folder Resources/Items/folder and load them in the shelf.
        /// </summary>
        /// <param name="folder">The folder where the items are.</param>
        public void OpenTab(string folder)
        {
            var items = Resources.LoadAll<SimpleItem>($"Items/{folder}").Select(item => item.itemData).ToArray();
            StartCoroutine(LoadItems(items, shelfTransform, AddToCart));
        }

        private void AddToCart(SimpleItem.ItemData item)
        {
            _cart.Add(item);
            total = _cart.Sum(i => i.price);
            totalText.text = $"{total}";
            RefreshCart();
        }
    
        internal void RemoveFromCart(SimpleItem.ItemData item)
        {
            _cart.Remove(item);
            total = _cart.Sum(i => i.price);
            totalText.text = $"{total}";
            RefreshCart();
        }
    
        /// <summary>
        /// Refreshes the cart slots with the _cart items.
        /// </summary>
        private void RefreshCart()
        {
            StartCoroutine(LoadItems(_cart.ToArray(), cartTransform, RemoveFromCart));
        }
    
        public void Buy()
        {
            if (money < total)
            {
                dialog.Write("You don't have enough money!");
                dialog.DelayedWrite("I don't think you may need more...", 3);
                return;
            }
            foreach (var item in _cart)
            {
                Inventory.Instance.AddItem(item);
            }
            money -= total;
            moneyText.text = $"{money}";
            _cart.Clear();
            total = 0;
            totalText.text = $"{total}";
            RefreshCart();
            
            dialog.Write("Thanks, enjoy!");
            dialog.DelayedWrite("Do yo need anything else?", 3);
        }
    }
}
