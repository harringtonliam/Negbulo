using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UseablePropControl;

namespace RPG.UI.UseableProps
{
    public class SleeperRoomControlsUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] Button closeButton;

        int nextIndicatorIndex = 0;

        UseablePropLink useablePropLink;
        GameObject player;

        void Start()
        {
            useablePropLink = FindObjectOfType<UseablePropLink>();
            useablePropLink.onDisplaySleeperRoomControlsUI += ShowDisplay;
            closeButton.onClick.AddListener(Close);

            player = GameObject.FindGameObjectWithTag("Player");

        }

        private void ShowDisplay()
        { 
            if (useablePropLink == null) return;


            player.GetComponent<UseProp>().onUsePropCancel += HideDisplay;

            //if (useablePropLink.CurrentUsableProp.ActivateText == string.Empty)
            //{
            //    actionCanvas.SetActive(false);
            //}
            //else
            //{
            //    actionCanvas.SetActive(true);
            //}

            uiCanvas.SetActive(true);
        }

        private void HideDisplay()
        {
            uiCanvas.SetActive(false);
        }

        private void Close()
        {
            player.GetComponent<UseProp>().Cancel();
            HideDisplay();
        }

    }
}


