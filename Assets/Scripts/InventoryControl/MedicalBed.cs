using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using RPG.UseablePropControl;


namespace RPG.InventoryControl
{
    public class MedicalBed : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] float kanarioHuntModeChaseDisatnce = 200f;
        [SerializeField] Transform whereToGetUniform;


        CrewMemberSettings crewMemberSettings;

        private Inventory inventory;

        // Start is called before the first frame update
        void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.inventoryUpdated += CheckForSleeper;
            crewMemberSettings = FindObjectOfType<CrewMemberSettings>();
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
                if (!crewMemberSettings.IsItemCrewMember(sleeper))
                {
                    SetPanicMode(newSleeper);
                    LockAllTheDoors();
                    SetKanarioToHunt();
                }
                else
                {
                    CrewMemberActions(newSleeper);
                }

            }
        }

        private void CrewMemberActions(GameObject crewMember)
        {
            //TODO.  Is this necessary anymore?
            //CrewMemberControler crewMemberController = crewMember.GetComponent<CrewMemberControler>();
            //crewMemberController.GetUniform(whereToGetUniform);
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


