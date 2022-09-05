using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UseablePropControl
{
    public class SleeperCoffinHatch : MonoBehaviour
    {

        [SerializeField] Transform hatchCover;
        [SerializeField] Transform coffinStartPosition;
        [SerializeField] Transform coffinEndPosition;
        [SerializeField] GameObject vfx;

        //Properties
        public Transform CoffinStartPosition { get { return coffinStartPosition; } }
        public Transform CoffinEndPosition { get { return coffinEndPosition; } }
        public Vector3 HatchStartPosition { get { return hatchStartPostion; } }
        public Vector3 HatchEndPosition { get { return hatchEndPosition; } }
        

        //Attributes
        Vector3 hatchStartPostion;
        Vector2 hatchEndPosition;

        // Start is called before the first frame update
        void Start()
        {
            hatchStartPostion = hatchCover.position;
            hatchEndPosition = hatchStartPostion + new Vector3(0, -2f, 0);
        }

        void Update()
        {

        }

        public void DisplayVFX(bool display)
        {
            vfx.SetActive(display);
        }

        public void DisplayHatchCover( bool display)
        {
            hatchCover.gameObject.SetActive(display);
        }

    }

}


