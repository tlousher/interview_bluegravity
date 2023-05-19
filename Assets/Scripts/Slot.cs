using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    
    public SimpleItem.ItemData item;
    public Image itemImage;
    public Image backgroundImage;
    
    public void Start()
    {
        if (item == null) return;
        itemImage.sprite = item.sprite;
        backgroundImage.sprite = Resources.Load<Sprite>("Frames/Frame_" + item.rarity);
    }
}
