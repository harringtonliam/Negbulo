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


        UseableProp currentUseableProp;

        public UseableProp CurrentUsableProp
        {
            get { return currentUseableProp; }
        }

        public void DisplayUsePropUI(UseableProp useableProp)
        {
            Debug.Log("DisplayUsePropUI");
            currentUseableProp = useableProp;

            SleeperRoomControls sleeperRoomControls = useableProp.GetComponent<SleeperRoomControls>();
            if(sleeperRoomControls)
            {
                if (onDisplaySleeperRoomControlsUI != null)
                {
                    onDisplaySleeperRoomControlsUI();
                }

            }
            else if (onDisplayUseablePropUI != null)
            {
                onDisplayUseablePropUI();
            }
        }


    }

}
