using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.Movement;

namespace RPG.Control

{
    public class CrewMemberControler : MonoBehaviour
    {

        [SerializeField] InventoryItem uniformInventoryItem;

        Equipment equipment;
        Mover mover;
        Vector3 starPosition;

        bool gettingUniform = false;



        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Crew member start");
            starPosition = transform.position;
            equipment = GetComponent<Equipment>();
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!gettingUniform)
            {
                GetUniform();
            }

        }

        private void GetUniform()
        {
            gettingUniform = true;

            if (equipment.GetItemInSlot(EquipLocation.Body) == uniformInventoryItem) return;

            Transform nearestContainer = GetNearestPickup();

            mover.StartMovementAction(nearestContainer.position, 1f);
        }


        private Transform GetNearestContainer()
        {
            Debug.Log("Get nearest container");
            Container[] container = FindObjectsOfType<Container>();
            float distanceToContainer;
            float shortestDistance = float.MaxValue;
            int nearestContainer = 0;
            for (int i = 0; i < container.Length; i++)
            {
                distanceToContainer = Vector3.Distance(container[i].transform.position, transform.position);
                Debug.Log("get nearest container distance =" + distanceToContainer.ToString() + " i=" + i.ToString());
                Inventory containerInventeory = container[i].GetComponent<Inventory>();
                if (containerInventeory.HasItem(uniformInventoryItem))
                {
                    if (distanceToContainer < shortestDistance && container[i].gameObject != gameObject)
                    {
                        Debug.Log("Setting shortest distance new=" + distanceToContainer + " old=" + shortestDistance.ToString());
                        shortestDistance = distanceToContainer;
                        nearestContainer = i;
                    }
                }
            }


            Debug.Log("nearest container index   nearestContainer = " + nearestContainer.ToString() + " gameobject=" + container[nearestContainer].gameObject);
            return container[nearestContainer].transform;

        }

        private Transform GetNearestPickup()
        {
            Pickup[] pickups = FindObjectsOfType<Pickup>();
            float distanceToPickup;
            float shortestDistance = float.MaxValue;
            int nearestPickup = 0;
            for (int i = 0; i < pickups.Length; i++)
            {

                if (pickups[i].InventoryItem == uniformInventoryItem)
                {
                    distanceToPickup = Vector3.Distance(pickups[i].transform.position, transform.position);
                    if (distanceToPickup < shortestDistance)
                    {
                        shortestDistance = distanceToPickup;
                        nearestPickup = i;
                    }
                }
            }
            return pickups[nearestPickup].transform;

        }
    }

}



