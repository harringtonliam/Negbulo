using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;
using RPG.Core;

namespace RPG.InventoryControl
{
    public class Equipment : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        Dictionary<EquipLocation, EquipableItem> equippedItems = new Dictionary<EquipLocation, EquipableItem>();

        public event Action equipmentUpdated;

        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if(!equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return equippedItems[equipLocation];
        }

        public Dictionary<EquipLocation, EquipableItem> GetEquippedItems()
        {
            return equippedItems;
        }

        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.AllowedEquiplocation == slot);

            equippedItems[slot] = item;

            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }

        }

        public void RemoveItem(EquipLocation slot)
        {
            equippedItems.Remove(slot);
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        public IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return equippedItems.Keys;
        }

        public object CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
            foreach (var pair in equippedItems)
            {
              equippedItemsForSerialization[pair.Key] = pair.Value.ItemID;
            }
            return equippedItemsForSerialization;
        }

        public void RestoreState(object state)
        {
            equippedItems = new Dictionary<EquipLocation, EquipableItem>();

            var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

            foreach (var pair in equippedItemsForSerialization)
            {
                var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                if (item != null)
                {
                    equippedItems[pair.Key] = item;
                }
            }
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {
            switch (predicate)
            {
                case PredicateType.EquipedInventoryItem:
                    return ItemEquiped(parameters[0]);
            }

            return null;
        }

        private bool ItemEquiped(string itemID)
        {
            InventoryItem inventoryItem = InventoryItem.GetFromID(itemID);
            EquipableItem equipableItem = (EquipableItem)inventoryItem;
            if(equipableItem == null)
            {
                return false;
            }

            if(GetItemInSlot(equipableItem.AllowedEquiplocation) == equipableItem)
            {
                return true;
            }

            return false;
        }
    }
}



