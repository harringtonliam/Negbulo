using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.VFX
{
    public class LightFader : MonoBehaviour
    {
        [SerializeField] Light light;
        [SerializeField] float minIntensity = 0f;
        [SerializeField] float fadeTimeSeconds = 2f;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (fadeTimeSeconds <= Mathf.Epsilon)  //protect against periodn being 0
            {
                return;
            }
            float cycles = Time.time / fadeTimeSeconds;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);
            float intensityFactor = (rawSinWave / 2f) + 0.5f;

            float offset =  intensityFactor;
            light.intensity = minIntensity + offset;
        }
    }


}

