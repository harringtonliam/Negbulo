using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.InventoryControl
{
    public class CharacterClothing : MonoBehaviour
    {
        [SerializeField]
        ClothingConfig[] clothingConfigs;


        [Serializable]
        public struct ClothingConfig
        {
            public InventoryItem inventoryItem;
            public Transform characterMesh;

        }

        Equipment equipment;

        // Start is called before the first frame update
        void Start()
        {
            equipment = GetComponent<Equipment>();
            equipment.equipmentUpdated += SetClothing;
            SetClothing();
        }


        void SetClothing()
        {
            InventoryItem bodyItem = equipment.GetItemInSlot(EquipLocation.Body);
            for (int i = 0; i < clothingConfigs.Length; i++)
            {
                    if (bodyItem == clothingConfigs[i].inventoryItem)
                    {
                        clothingConfigs[i].characterMesh.gameObject.SetActive(true);
                    }
                    else
                    {
                        clothingConfigs[i].characterMesh.gameObject.SetActive(false);
                    }
            }

        }
    }
}


