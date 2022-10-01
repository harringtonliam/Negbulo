using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UseablePropControl
{

    public class UseablePropLink : MonoBehaviour
    {

        public event Action onDisplayUseablePropUI;
        public event Action onDisplaySleeperRoomControlsUI;
        public event Action onDisplayLiftControlsUI;


        UseableProp currentUseableProp;

        public UseableProp CurrentUsableProp
        {
            get { return currentUseableProp; }
        }

        public void DisplayUsePropUI(UseableProp useableProp)
        {
            currentUseableProp = useableProp;

            SleeperRoomControls sleeperRoomControls = useableProp.GetComponent<SleeperRoomControls>();
            LiftControls liftControls = useableProp.GetComponent<LiftControls>();

            if (sleeperRoomControls)
            {
                if (onDisplaySleeperRoomControlsUI != null)
                {
                    onDisplaySleeperRoomControlsUI();
                }

            }
            else if(liftControls)
            {
                if (onDisplayLiftControlsUI != null)
                {
                    onDisplayLiftControlsUI();
                }
            }
            else if (onDisplayUseablePropUI != null)
            {
                onDisplayUseablePropUI();
            }
        }


    }

}
