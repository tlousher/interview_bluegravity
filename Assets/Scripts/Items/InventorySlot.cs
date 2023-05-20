using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items
{
    public class InventorySlot : Slot
    {
        public TextMeshProUGUI amountText;
        public GameObject amountKnob;
        public Image frameImage;
        public Color normalFrameColor = new Color(0.4078432f, 0.3686275f, 0.3058824f, 1);
        public Color equippedFrameColor = Color.green;
        
        internal void SetFrameColor(bool equipped)
        {
            frameImage.color = equipped ? equippedFrameColor : normalFrameColor;
        }
    }
}
