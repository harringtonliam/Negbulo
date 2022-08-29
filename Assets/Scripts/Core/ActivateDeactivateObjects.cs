using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActivateDeactivateObjects : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToActivate;
        [SerializeField] GameObject[] objectsToDeactivate;

        public void ActivatetDeactivate(bool activate)
        {
            Debug.Log("Activate deactivate");

            foreach (var item in objectsToActivate)
            {
                item.SetActive(activate);
            }

            foreach (var item in objectsToDeactivate)
            {
                item.SetActive(!activate);
            }


        }



    }

}


