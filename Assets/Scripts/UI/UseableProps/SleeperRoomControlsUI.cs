using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UseablePropControl;
using System;

namespace RPG.UI.UseableProps
{
    public class SleeperRoomControlsUI : MonoBehaviour
    {

        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] Button closeButton;
        [SerializeField] GameObject indicatorLightPrefab = null;
        [SerializeField] Transform indicatorLightsPanel = null;

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


        public void ResetControls()
        {
            SleeperRoomControls sleeperRoomControls = (SleeperRoomControls)useablePropLink.CurrentUsableProp;
            sleeperRoomControls.ResetControls();
        }


        private void ShowDisplay()
        { 
            if (useablePropLink == null) return;

            player.GetComponent<UseProp>().onUsePropCancel += HideDisplay;

            RedrawIndicaorLights();

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

        private void RedrawIndicaorLights()
        {
            foreach (Transform child in indicatorLightsPanel)
            {
                Destroy(child.gameObject);
            }

            SleeperRoomControls sleeperRoomControls = (SleeperRoomControls)useablePropLink.CurrentUsableProp;

            for (int i = 0; i < sleeperRoomControls.SelectedButtonColors.Length; i++)
            {
                var indicatorLight = Instantiate(indicatorLightPrefab, indicatorLightsPanel);
                indicatorLight.GetComponent<Image>().color = GetButtonColor(sleeperRoomControls.SelectedButtonColors[i]);
            }
        }

        private Color GetButtonColor(ButtonColor buttonColor)
        {
            Color newButtonColor = Color.black;
            switch (buttonColor)
            {
                case ButtonColor.Black:
                    newButtonColor =  Color.black;
                    break;
                case ButtonColor.White:
                    newButtonColor = Color.white;
                    break;
                case ButtonColor.Gray:
                    newButtonColor = Color.gray;
                    break;
                case ButtonColor.Green:
                    newButtonColor = Color.green;
                    break;
                case ButtonColor.Blue:
                    newButtonColor = Color.blue;
                    break;
                case ButtonColor.Yellow:
                    newButtonColor = Color.yellow;
                    break;
                case ButtonColor.Cyan:
                    newButtonColor = Color.cyan;
                    break;
                case ButtonColor.Magenta:
                    newButtonColor = Color.magenta;
                    break;
                case ButtonColor.Red:
                    newButtonColor = Color.red;
                    break;
                case ButtonColor.Orange:
                    Color orangeColor = new Color(1f, 0.647f, 0f);
                    newButtonColor = orangeColor;
                    break;
                default:
                    newButtonColor = Color.black;
                    break;

            }
            return newButtonColor;
        }
    }
}


