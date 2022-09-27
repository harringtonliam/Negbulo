using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.VFX
{

    public class ObjectOnOff : MonoBehaviour
    {
        [SerializeField] float offTimeSeconds = 2f;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ObjectShowHide());
        }

        private IEnumerator ObjectShowHide()
        {

            while (true)
            {
                yield return new WaitForSeconds(offTimeSeconds);
                GetComponent<Renderer>().enabled =  !GetComponent<Renderer>().enabled;
            }

        }


    }

}

