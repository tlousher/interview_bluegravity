using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int price;
        public Sprite sprite;
        [Range(1, 5)]
        public int rarity;
    }
    
    public Item item;
    public Image itemImage;
    public Image frameImage;
    
    public void Start()
    {
        itemImage.sprite = item.sprite;
        frameImage.sprite = Resources.Load<Sprite>("Sprites/Items/Frame_" + item.rarity);
    }
}
