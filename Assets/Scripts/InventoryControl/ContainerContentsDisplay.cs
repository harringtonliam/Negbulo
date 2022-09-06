using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.InventoryControl
{

    public class ContainerContentsDisplay : MonoBehaviour
    {
        [SerializeField] Transform pickupPoint;

        Inventory inventory;

        Pickup pickup = null;

        // Start is called before the first frame update
        void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.inventoryUpdated += ShowHidePickup;
        }


        private void ShowHidePickup()
        {
            if (pickup != null)
            {
                Destroy(pickup.gameObject);
            }

            if (pickupPoint == null) return;

            for (int i = 0; i < inventory.GetSize(); i++)
            {
                InventoryItem item = inventory.GetItemInSlot(i);
                if(item != null)
                {
                    pickup = item.SpawnPickup(pickupPoint.position, 1);
                    pickup.transform.parent = pickupPoint.transform;
                    pickup.transform.rotation = pickupPoint.transform.rotation;
                }
            }
        }
    }
}



