using UnityEngine;

public class SimpleItem : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public string name;
        public string description;
        public int price;
        public Sprite sprite;
        [Range(1, 5)]
        public int rarity = 1;
    }
    
    public ItemData itemData;
}
