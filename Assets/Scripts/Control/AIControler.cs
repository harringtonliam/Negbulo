using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using RPG.Attributes;
using RPG.InventoryControl;

namespace RPG.Control
{
    public class AIControler : MonoBehaviour
    {
        [SerializeField] AIRelationship aIRelationship = AIRelationship.Hostile;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] float aggrevationCoolDownTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointPauseTime = 2f;
        [Range(0f, 1f)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float shoutDistance = 5f;
        [SerializeField] GameObject combatTargetGameObject;
        [SerializeField] Transform panicDestination;
        [SerializeField] float panicDestinationTolerance = 5f;
        [SerializeField] InventoryItem itemToPickup;

        GameObject player;
        Mover mover;


        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        float timeAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        bool panic = false;
        Pickup itemToPickupPickup;
        Container itemToPickupContainer;
        bool pickupLoactionFound = false;


        public AIRelationship AIRelationship
        {
            get{ return aIRelationship;}
        }

        public bool Panic { get { return panic; } set { panic = value; } }

        public Transform PanicDestination { get { return panicDestination; }  set { panicDestination = value; } }

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            if (aIRelationship == AIRelationship.Hostile)
            {
                combatTargetGameObject = player;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
              guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;

            if (GetComponent<Health>().IsDead) return;

            if (InteractWithPanic()) return;
            if (InteractWithCombat()) return;
            if (InteractwithInventoryItem()) return;
            if (InteractWithSuspicsion()) return;
            if (InteractWithPatrolPath()) return;
            if (InteractWithGuardPosition()) return;
        }



        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        public void SetChaseDistance(float newChaseDistance)
        {
            chaseDistance = newChaseDistance;
        }

        public void SetPatrolPath(PatrolPath newPatrolPath)
        {
            patrolPath = newPatrolPath;
            currentWaypointIndex = 0;
        }
        private bool InteractWithPanic()
        {
            if (!panic) return false;
            if (AtPanicDestination()) return false; ;
            mover.StartMovementAction(panicDestination.position, 1f);
            return panic;
        }

        private bool AtPanicDestination()
        {
            float distanceToPanciDestination = Vector3.Distance(transform.position, panicDestination.position);
            if (distanceToPanciDestination <= panicDestinationTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetCombatTarget(GameObject target)
        {
            combatTargetGameObject = target;
        }

        private bool InteractWithPatrolPath()
        {
            if (patrolPath == null) return false;

            timeAtWaypoint += Time.deltaTime;

            if (AtWaypoint())
            {
                timeAtWaypoint = 0;
                CycleWaypoint();
            }

            if (timeAtWaypoint > waypointPauseTime)
            {
                mover.StartMovementAction(GetCurrentWaypoint(), patrolSpeedFraction);
            }

            return true;
        }

        private bool AtWaypoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            if (distanceToWayPoint <= waypointTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool InteractwithInventoryItem()
        {
            Vector3 nearestItemLocation;

            if (itemToPickup == null)
            {
                return false;
            }

            pickupLoactionFound = false;
            itemToPickupPickup = GetNearestPickup();
            itemToPickupContainer = GetNearestContainer();
            if (!pickupLoactionFound) return false;
            if (itemToPickupPickup != null && itemToPickupContainer == null)
            {
                nearestItemLocation = itemToPickupPickup.transform.position;
            }
            else if (itemToPickupPickup == null && itemToPickupContainer != null)
            {
                nearestItemLocation = itemToPickupContainer.transform.position;
            }
            else if (Vector3.Distance(itemToPickupPickup.transform.position, transform.position)<= Vector3.Distance(itemToPickupContainer.transform.position, transform.position))
            {
                nearestItemLocation = itemToPickupPickup.transform.position;
                itemToPickupContainer = null;
            }
            else
            {
                nearestItemLocation = itemToPickupContainer.transform.position;
                itemToPickupPickup = null;
            }
            
            mover.StartMovementAction(nearestItemLocation, 1f);

            if (AtItemLocation(nearestItemLocation))
            {
                Debug.Log("At Item Location");
                PickupInventoryItem();
                return false;
            }

            return true;
        }

        private void PickupInventoryItem()
        {
            Debug.Log("PickupInventoryItem");

            InventoryItem inventoryItem;

            if (itemToPickupPickup != null)
            {
                inventoryItem = itemToPickupPickup.InventoryItem;
                Destroy(itemToPickupPickup.gameObject);

            }
            else if (itemToPickupContainer != null)
            {
                Debug.Log("PickupInventoryItem container " + itemToPickupContainer.gameObject);
                inventoryItem = itemToPickup;
                Inventory containerInventory = itemToPickupContainer.GetComponent<Inventory>();
                containerInventory.RemoveItem(inventoryItem, 1);
            }
            else
            {
                return;
            }

            EquipableItem equipableItem = (EquipableItem)inventoryItem;
            Equipment equipment = GetComponent<Equipment>();

            if (equipableItem != null && equipment != null)
            {
                equipment.AddItem(equipableItem.AllowedEquiplocation, equipableItem);
                
            }
            else
            {
                Inventory inventory = GetComponent<Inventory>();
                inventory.AddToFirstEmptySlot(inventoryItem, itemToPickupPickup.NumberOfItems);
            }

            itemToPickup = null;
        }

        private bool AtItemLocation(Vector3 itemLocation)
        {
            float distanceToItemLocation = Vector3.Distance(transform.position, itemLocation);
            Debug.Log("distance to Item Location " + distanceToItemLocation.ToString());
            if (distanceToItemLocation <= waypointTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InteractWithSuspicsion()
        {
            if (timeSinceLastSawPlayer < suspicionTime )
            {
                ActionScheduler actionSchduler = GetComponent<ActionScheduler>();
                actionSchduler.CancelCurrentAction();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InteractWithGuardPosition()
        {
            mover.StartMovementAction(guardPosition, patrolSpeedFraction);
            return true;
        }

        private bool InteractWithCombat()
        {

            Fighting fighter = GetComponent<Fighting>();
            if (combatTargetGameObject == null)
            {
                fighter.Cancel();
                return false;
            }
            if (IsAggrevated() && fighter.CanAttack(combatTargetGameObject))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(combatTargetGameObject);
                AggrevateNearbyEnemies();
                return true;
            }
            else
            {
                fighter.Cancel();
                return false;
            }
        }

        private void AggrevateNearbyEnemies()
        {
            if (aIRelationship != AIRelationship.Hostile) return;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0f);
            foreach (var hit in hits)
            {

                AIControler ai = hit.transform.GetComponent<AIControler>();
                if (ai != null && ai != this)
                {
                    ai.Aggrevate();
                }
            }

        }

        private bool IsAggrevated()
        {
            if (timeSinceAggrevated < aggrevationCoolDownTime)
            {
                //aggrevated
                return true;
            }
            return DistanceToCombatTarget() <= chaseDistance;
        }

        private float DistanceToCombatTarget()
        {
            if (combatTargetGameObject == null) return Mathf.Infinity;
            float distance = Vector3.Distance(combatTargetGameObject.transform.position, transform.position);
            return distance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private Container GetNearestContainer()
        {
            Container[] container = FindObjectsOfType<Container>();
            float distanceToContainer;
            float shortestDistance = float.MaxValue;
            int nearestContainer = 0;
            for (int i = 0; i < container.Length; i++)
            {
                distanceToContainer = Vector3.Distance(container[i].transform.position, transform.position);
                Inventory containerInventeory = container[i].GetComponent<Inventory>();
                if (containerInventeory.HasItem(itemToPickup))
                {
                    if (distanceToContainer < shortestDistance && container[i].gameObject != gameObject)
                    {
                        shortestDistance = distanceToContainer;
                        nearestContainer = i;
                        pickupLoactionFound = true;
                    }
                }
            }
            return container[nearestContainer];

        }

        private Pickup GetNearestPickup()
        {
            Pickup[] pickups = FindObjectsOfType<Pickup>();
            float distanceToPickup;
            float shortestDistance = float.MaxValue;
            int nearestPickup = 0;
            for (int i = 0; i < pickups.Length; i++)
            {
                if (pickups[i].InventoryItem == itemToPickup)
                {
                    distanceToPickup = Vector3.Distance(pickups[i].transform.position, transform.position);
                    if (distanceToPickup < shortestDistance)
                    {
                        shortestDistance = distanceToPickup;
                        nearestPickup = i;
                        pickupLoactionFound = true;
                    }
                }
            }
            return pickups[nearestPickup];

        }
    }
}
