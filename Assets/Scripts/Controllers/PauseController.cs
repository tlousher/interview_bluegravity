using System;
using Items;
using UnityEngine;

namespace Controllers
{
    public class PauseController : MonoBehaviour
    {
        public Transform inventoryTransform;
        public RectTransform pauseMenuTransform;
        public GameObject inventoryPanel;
        public GameObject controlsPanel;
        public GameObject slotPrefab;

        private Canvas _canvas;
        private EquipmentController _equipmentController;

        public void Start()
        {
            _canvas = GetComponent<Canvas>();
            _equipmentController = FindObjectOfType<EquipmentController>();
            ClosePanels();
            _canvas.enabled = false;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_canvas.enabled)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                    OpenInventory();
                }
            }
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_canvas.enabled)
                Unpause();
            else
                Pause();
        }

        /// <summary>
        /// Open the inventory panel and load the items.
        /// </summary>
        public void OpenInventory()
        {
            controlsPanel.SetActive(false);
            inventoryPanel.SetActive(true);
            pauseMenuTransform.anchoredPosition = new Vector2(-300, -100);
            
            LoadItems();
        }

        private void LoadItems()
        {
            foreach (var inventorySlot in Inventory.Instance.Items)
            {
                var slot = Instantiate(slotPrefab, inventoryTransform);
                // Moves the slot to the right position with an offset of 110 on x and 110 on y every 4 slots instantiated.
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    slot.transform.GetSiblingIndex() % 4 * 110,
                    -110 * (slot.transform.GetSiblingIndex() / 4) - 10
                );
                var slotData = slot.GetComponent<InventorySlot>();
                slotData.item = inventorySlot.Value.Item;
                slotData.amountText.SetText($"{inventorySlot.Value.Quantity}");
                slotData.amountKnob.SetActive(inventorySlot.Value.Quantity > 1);
                slotData.OnSlotClick = EquipItem;
                slotData.SetFrameColor(inventorySlot.Value.Equipped);
            }

            var parentRectTransform = inventoryTransform.GetComponent<RectTransform>();
            parentRectTransform.sizeDelta = new Vector2(
                parentRectTransform.sizeDelta.x,
                110 * (inventoryTransform.childCount / 4 + 1)
            );
        }

        public void OpenControls()
        {
            inventoryPanel.SetActive(false);
            controlsPanel.SetActive(true);
            pauseMenuTransform.anchoredPosition = new Vector2(-300, -100);
        }
        
        /// <summary>
        /// Clear the inventory and close the inventory and controls panel, also reset the pause menu position.
        /// </summary>
        public void ClosePanels()
        {
            UnloadItems();
            inventoryPanel.SetActive(false);
            controlsPanel.SetActive(false);
            pauseMenuTransform.anchoredPosition = new Vector2(0, -100);
        }

        private void UnloadItems()
        {
            foreach (Transform child in inventoryTransform)
            {
                Destroy(child.gameObject);
            }
        }

        public void Unpause()
        {
            ClosePanels();
            _canvas.enabled = false;
            MovementController.CanMove.Remove("Pause");
        }

        public void Pause()
        {
            _canvas.enabled = true;
            MovementController.CanMove.Add("Pause");
        }

        private void EquipItem(SimpleItem.ItemData item)
        {
            UnloadItems();
            Invoke(nameof(LoadItems), 0.01f);
            _equipmentController.EquipItem(item);
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}
