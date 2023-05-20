using System;
using System.Linq;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class EquipmentController : MonoBehaviour
    {
        public SpriteRenderer[] bodyArmor = new SpriteRenderer[4];
        public SpriteRenderer[] leftArmArmor = new SpriteRenderer[4];
        public SpriteRenderer[] rightArmArmor = new SpriteRenderer[4];
        public SpriteRenderer[] leftBootsArmor = new SpriteRenderer[4];
        public SpriteRenderer[] rightBootsArmor = new SpriteRenderer[4];
        public SpriteRenderer[] helmetArmor = new SpriteRenderer[4];
        [FormerlySerializedAs("shieldArmor")] public SpriteRenderer[] shield = new SpriteRenderer[4];
        [FormerlySerializedAs("swordArmor")] public SpriteRenderer[] sword = new SpriteRenderer[4];
        
        public void EquipItem(SimpleItem.ItemData item)
        {
            switch (item.itemType)
            {
                case SimpleItem.ItemType.Armor:
                    EquipArmor(item);
                    break;
                case SimpleItem.ItemType.Boots:
                    EquipBoots(item);
                    break;
                case SimpleItem.ItemType.Helmet:
                    EquipHelmet(item);
                    break;
                case SimpleItem.ItemType.Shield:
                    EquipShield(item);
                    break;
                case SimpleItem.ItemType.Sword:
                    EquipSword(item);
                    break;
            }
            
            foreach (var inventoryItem in Inventory.Instance.Items.Where(inventoryItem => inventoryItem.Value.Item.itemType == item.itemType && inventoryItem.Value.Equipped))
            {
                inventoryItem.Value.Equipped = false;
            }
            Inventory.Instance.Items[item.name].Equipped = true;
        }

        private void EquipSword(SimpleItem.ItemData item)
        {
            try
            {
                sword[0].sprite = item.sprites[1];
                sword[1].sprite = item.sprites[1];
                sword[2].sprite = item.sprites[1];
                sword[3].sprite = item.sprites[1];
            }
            catch (Exception)
            {
                Debug.Log($"Error while equipping sword: {item.name}");
            }
        }

        private void EquipShield(SimpleItem.ItemData item)
        {
            try
            {
                shield[0].sprite = item.sprites[1];
                shield[1].sprite = item.sprites[2];
                shield[2].sprite = item.sprites[1];
                shield[3].sprite = item.sprites[2];
            }
            catch (Exception)
            {
                Debug.Log($"Error while equipping shield: {item.name}");
            }
        }

        private void EquipHelmet(SimpleItem.ItemData item)
        {
            try
            {
                helmetArmor[0].sprite = item.sprites[1];
                helmetArmor[1].sprite = item.sprites[2];
                helmetArmor[2].sprite = item.sprites[3];
                helmetArmor[3].sprite = item.sprites[3];
            }
            catch (Exception)
            {
                Debug.Log($"Error while equipping helmet: {item.name}");
            }
        }

        private void EquipBoots(SimpleItem.ItemData item)
        {
            try
            {
                leftBootsArmor[0].sprite = item.sprites[1];
                leftBootsArmor[1].sprite = item.sprites[2];
                leftBootsArmor[2].sprite = item.sprites[3];
                leftBootsArmor[3].sprite = item.sprites[3];
                rightBootsArmor[0].sprite = item.sprites[4];
                rightBootsArmor[1].sprite = item.sprites[5];
                rightBootsArmor[2].sprite = item.sprites[6];
                rightBootsArmor[3].sprite = item.sprites[6];
            }
            catch (Exception)
            {
                Debug.Log($"Error while equipping boots: {item.name}");
            }
        }

        private void EquipArmor(SimpleItem.ItemData item)
        {
            try
            {
                bodyArmor[0].sprite = item.sprites[1];
                bodyArmor[1].sprite = item.sprites[2];
                bodyArmor[2].sprite = item.sprites[3];
                bodyArmor[3].sprite = item.sprites[3];
                leftArmArmor[0].sprite = item.sprites[4];
                leftArmArmor[1].sprite = item.sprites[5];
                leftArmArmor[2].sprite = item.sprites[6];
                leftArmArmor[3].sprite = item.sprites[6];
                rightArmArmor[0].sprite = item.sprites[7];
                rightArmArmor[1].sprite = item.sprites[8];
                rightArmArmor[2].sprite = item.sprites[9];
                rightArmArmor[3].sprite = item.sprites[9];
            }
            catch (Exception)
            {
                Debug.Log($"Error while equipping armor: {item.name}");
            }
        }
    }
}
