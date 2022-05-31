using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using System;

namespace RPG.UI.InventoryControl
{



    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlotUI inventorySlotUIPrefab = null;


        Inventory objectInventory;

        public void SetInventoryObject(Inventory inventory)
        {
            objectInventory = inventory;
            objectInventory.inventoryUpdated += Redraw;
            Redraw();
        }

        private void Awake()
        {
            objectInventory = Inventory.GetPlayerInventory();
            objectInventory.inventoryUpdated += Redraw;
        }

        void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < objectInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(inventorySlotUIPrefab, transform);
                itemUI.Setup(objectInventory, i);
            }
        }



 
    }



}


