using System;
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

        public void Start()
        {
            _canvas = GetComponent<Canvas>();
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

        public void OpenInventory()
        {
            controlsPanel.SetActive(false);
            inventoryPanel.SetActive(true);
            pauseMenuTransform.anchoredPosition = new Vector2(-300, -100);
            
            foreach (var inventorySlot in Inventory.Instance.Items)
            {
                var slot = Instantiate(slotPrefab, inventoryTransform);
                // Moves the slot to the right position with an offset of 110 on x and 110 on y every 4 slots instantiated.
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    slot.transform.GetSiblingIndex() % 4 * 110,
                    -110 * (slot.transform.GetSiblingIndex() / 4)
                );
                var slotData = slot.GetComponent<Slot>();
                slotData.item = inventorySlot.Value.Item;
                slotData.OnSlotClick = EquipItem;
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
        
        public void ClosePanels()
        {
            inventoryPanel.SetActive(false);
            controlsPanel.SetActive(false);
            pauseMenuTransform.anchoredPosition = new Vector2(0, -100);
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

        public void EquipItem(SimpleItem.ItemData item)
        {
            Debug.Log($"Equipping {item.name}");
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}
