using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.UI.Dragging;

namespace RPG.UI.InventoryControl
{
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        //Configuration
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        Equipment playerEquipment;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }

        void Start()
        {
            RedrawUI();
        }


        public void AddItems(InventoryItem item, int number)
        {
            playerEquipment.AddItem(equipLocation, (EquipableItem)item);
        }

        public InventoryItem GetItem()
        {
            return playerEquipment.GetItemInSlot(equipLocation);
        }

        public int GetNumber()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int MaxAcceptable(InventoryItem item)
        {
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.AllowedEquiplocation != equipLocation) return 0;
            if (GetItem() != null) return 0;

            return 1;
        }

        public void RemoveItems(int number)
        {
            playerEquipment.RemoveItem(equipLocation);
        }


        void RedrawUI()
        {
            icon.SetItem(playerEquipment.GetItemInSlot(equipLocation));
        }
    }
}


