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
                switch (sleeperRoomControls.SelectedButtonColors[i])
                {
                    case ButtonColor.Black:
                        indicatorLight.GetComponent<Image>().color = Color.black;
                        break;
                    case ButtonColor.White:
                        indicatorLight.GetComponent<Image>().color = Color.white;
                        break;
                    case ButtonColor.Gray:
                        indicatorLight.GetComponent<Image>().color = Color.gray;
                        break;
                    case ButtonColor.Green:
                        indicatorLight.GetComponent<Image>().color = Color.green;
                        break;
                    case ButtonColor.Blue:
                        indicatorLight.GetComponent<Image>().color = Color.blue;
                        break;
                    case ButtonColor.Yellow:
                        indicatorLight.GetComponent<Image>().color = Color.yellow;
                        break;
                    case ButtonColor.Cyan:
                        indicatorLight.GetComponent<Image>().color = Color.cyan;
                        break;
                    case ButtonColor.Magenta:
                        indicatorLight.GetComponent<Image>().color = Color.magenta;
                        break;
                    case ButtonColor.Red:
                        indicatorLight.GetComponent<Image>().color = Color.red;
                        break;
                    case ButtonColor.Orange:
                        indicatorLight.GetComponent<Image>().color = Color.red;
                        break;
                    default:
                        indicatorLight.GetComponent<Image>().color = Color.clear;
                        break;

                }

            }
        }

    }
}


