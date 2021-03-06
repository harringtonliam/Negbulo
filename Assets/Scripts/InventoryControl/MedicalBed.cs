using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;
using RPG.Movement;


namespace RPG.InventoryControl
{
    public class MedicalBed : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] float kanarioHuntModeChaseDisatnce = 200f;


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
                SetPanicMode(newSleeper); 
                LockAllTheDoors();
                SetKanarioToHunt();
            }
        }

        private void SetKanarioToHunt()
        {
            GameObject[] kanarios = GameObject.FindGameObjectsWithTag("Kanario");
            foreach (var kanario in kanarios)
            {
                kanario.GetComponent<AIControler>().SetChaseDistance(kanarioHuntModeChaseDisatnce);
                kanario.GetComponent<Mover>().MaxPathLength = kanarioHuntModeChaseDisatnce;
            }
           
        }

        private void LockAllTheDoors()
        {
            Door[] doors = FindObjectsOfType<Door>();
            foreach(Door door in doors)
            {
                door.Lock();
            }
        }

        private static void SetPanicMode(GameObject newSleeper)
        {
            AIControler aIControler = newSleeper.GetComponent<AIControler>();
            aIControler.PanicDestination = GameObject.FindGameObjectWithTag("Kanario").transform;
            aIControler.Panic = true;
        }
    }
}


