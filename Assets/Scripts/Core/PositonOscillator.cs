using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PositonOscillator : MonoBehaviour
    {
        //Properties
        [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
        [SerializeField] float period = 2f;


        //Member Variables
        Vector3 startingPos;

        // Start is called before the first frame update
        void Start()
        {
            startingPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (period <= Mathf.Epsilon)  //protect against periodn being 0
            {
                return;
            }
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);
            float movementFactor = (rawSinWave / 2f) + 0.5f;

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
    }
}


