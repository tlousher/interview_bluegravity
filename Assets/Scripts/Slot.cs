using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    
    public SimpleItem.ItemData item;
    public Image itemImage;
    public Image backgroundImage;
    
    internal UnityAction<SimpleItem.ItemData> OnSlotClick;
    
    public void Start()
    {
        if (item == null) return;
        itemImage.sprite = item.sprite;
        backgroundImage.sprite = Resources.Load<Sprite>("Frames/Frame_" + item.rarity);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClick?.Invoke(item);
    }
}
