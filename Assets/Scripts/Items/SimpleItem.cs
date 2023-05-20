using UnityEngine;

namespace Items
{
    public class SimpleItem : MonoBehaviour
    {
        [System.Serializable]
        public class ItemData
        {
            public string name;
            public string description;
            public int price;
            [Tooltip("Icon, Front, Back, Side")]
            public Sprite[] sprites = new Sprite[4];
            public ItemType itemType;
            [Range(1, 5)]
            public int rarity = 1;
        }
        
        public enum ItemType
        {
            Armor,
            Boots,
            Helmet,
            Sword,
            Shield
        }
    
        public ItemData itemData;
    }
}
