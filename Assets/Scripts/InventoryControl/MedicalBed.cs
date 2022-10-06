using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using RPG.UseablePropControl;
using RPG.SceneManagement;


namespace RPG.InventoryControl
{
    public class MedicalBed : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] float kanarioHuntModeChaseDisatnce = 200f;
        [SerializeField] float timeToWaitBeforeWaking = 2f;
        [SerializeField] GameObject lights;



        CrewMemberSettings crewMemberSettings;
        Pickup pickup;

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
                if (inventory.GetItemInSlot(i) != null && inventory.GetItemInSlot(i).GetType() == typeof(Sleeper))
                {
                    sleeper = (Sleeper)inventory.GetItemInSlot(i);
                }
                if (sleeper != null)
                {
                    StartCoroutine(WakeSleeper(sleeper, i));
                }
            }
        }

        private IEnumerator WakeSleeper(Sleeper sleeper, int inventorySlot)
        {
            if (lights != null)
            {
                lights.SetActive(true);
            }
            yield return new WaitForSeconds(timeToWaitBeforeWaking);
            GameObject sleeperPrefab = sleeper.CharacterPrefabToSpawn;
            if (sleeperPrefab != null)
            {
                GameObject.Destroy(pickup);
                GameObject newSleeper = Instantiate(sleeperPrefab, spawnPoint.position, Quaternion.identity);
                inventory.RemoveFromSlot(inventorySlot, 1);
                newSleeper.transform.parent = FindObjectOfType<SceneCharacters>().transform;
                if (!crewMemberSettings.IsItemCrewMember(sleeper))
                {
                    SetPanicMode(newSleeper);
                    LockAllTheDoors();
                    SetKanarioToHunt();
                    RedAlertLights();
                }
                else
                {
                    RememberCrewMember(newSleeper);
                }
            }
            if (lights != null)
            {
                lights.SetActive(false);
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

        private void RedAlertLights()
        {
            ActivateDeactivateObjects activateDeactivateObjects = GetComponent<ActivateDeactivateObjects>();
            if (activateDeactivateObjects != null)
            {
                activateDeactivateObjects.ActivatetDeactivate(true);
            }

        }

        private void RememberCrewMember(GameObject crewMember)
        {
            SceneCharacters sceneCharacters = FindObjectOfType<SceneCharacters>();
            sceneCharacters.WakeCrewMember(crewMember.transform.position);
        }


    }
}


