using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    private readonly Dictionary<string, InventorySlot> _items = new();

    public class InventorySlot
    {
        public SimpleItem.ItemData Item;
        public int Quantity;
    }
    
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
            }

            return _instance;
        }
    }
    
    public Dictionary<string, InventorySlot> Items => _items;

    public void AddItem(SimpleItem.ItemData item)
    {
        if (_items.TryGetValue(item.name, out var slot))
        {
            slot.Quantity++;
        }
        else
        {
            _items.Add(item.name, new InventorySlot {Item = item, Quantity = 1});
        }
    }
    
    public void RemoveItem(SimpleItem.ItemData item)
    {
        if (!_items.ContainsKey(item.name)) return;
        _items[item.name].Quantity--;
        if (_items[item.name].Quantity <= 0)
        {
            _items.Remove(item.name);
        }
    }
    
    public void RemoveItem(string itemName)
    {
        if (!_items.ContainsKey(itemName)) return;
        _items[itemName].Quantity--;
        if (_items[itemName].Quantity <= 0)
        {
            _items.Remove(itemName);
        }
    }
    
    public int GetQuantity(SimpleItem.ItemData item)
    {
        return _items.TryGetValue(item.name, out var slot) ? slot.Quantity : 0;
    }
    
    public int GetQuantity(string itemName) => _items.TryGetValue(itemName, out var item) ? item.Quantity : 0;
}
