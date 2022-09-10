using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using System;

namespace RPG.UseablePropControl
{

    public class UseablePropGiveItem : UseableProp
    {
        [SerializeField] ItemsToGive[] itemsToGive;

        [Serializable]
        public struct ItemsToGive
        {
            public InventoryItem inventoryItem;
            public int number;
        }




        public override void SetPropActivatedStatus(bool activatedStatus)
        {
            isActivated = activatedStatus;
            isDisabled = true;

            Inventory inventory = GetPlayerInventory();

            foreach (var item in itemsToGive)
            {
                inventory.AddToFirstEmptySlot(item.inventoryItem, item.number);
            }

            WriteToConsole(onActivatedText);

            if (isActivated && activateSound != null)
            {
                activateSound.Play();
            }
            else if (!isActivated && deactivateSound != null)
            {
                deactivateSound.Play();
            }

        }

        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }
    }
}


