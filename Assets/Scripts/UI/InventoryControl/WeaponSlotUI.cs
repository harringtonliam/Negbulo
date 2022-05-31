using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.UI.Dragging;
using RPG.Combat;

namespace RPG.UI.InventoryControl
{

    public class WeaponSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;

        // CACHE
        WeaponStore weaponStore;

        // LIFECYCLE METHODS
        private void Awake()
        {
            weaponStore = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponStore>();
            weaponStore.storeUpdated += UpdateIcon;
        }

        // PUBLIC

        public void AddItems(InventoryItem item, int number)
        {
            weaponStore.AddAction(item, index, number, true);
        }

        public InventoryItem GetItem()
        {
            return weaponStore.GetAction(index);
        }

        public int GetNumber()
        {
            return weaponStore.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return weaponStore.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            weaponStore.RemoveItems(index, number);
        }

        // PRIVATE

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }


}


