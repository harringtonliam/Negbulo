using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.InventoryControl
{
    public class MedicalBed : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;


        private Inventory inventory;

        // Start is called before the first frame update
        void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.inventoryUpdated += CheckForSleeper;
        }

        private void CheckForSleeper()
        {
            Sleeper sleeper = null;

            for (int i = 0; i < inventory.GetSize(); i++)
            {
                sleeper = (Sleeper)inventory.GetItemInSlot(i);
                if (sleeper != null)
                {
                    WakeSleeper(sleeper);
                    inventory.RemoveFromSlot(i, 1);
                }
            }
        }

        private void WakeSleeper(Sleeper sleeper)
        {
            GameObject sleeperPrefab = sleeper.CharacterPrefabToSpawn;
            if (sleeperPrefab != null)
            {
                GameObject newSleeper = Instantiate(sleeperPrefab, spawnPoint.position, Quaternion.identity);
                newSleeper.transform.parent = this.transform;

            }
        }
    }
}


