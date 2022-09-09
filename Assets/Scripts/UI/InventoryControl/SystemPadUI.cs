using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.InventoryControl;
using RPG.UseablePropControl;


namespace RPG.UI.InventoryControl
{
    public class SystemPadUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] Button closeButton;
        [SerializeField] GameObject indicatorLightPrefab = null;
        [SerializeField] Transform indicatorLightsPanel = null;


        ActionItemLink actionItemLink;

        // Start is called before the first frame update
        void Start()
        {
            actionItemLink = FindObjectOfType<ActionItemLink>();
            actionItemLink.onSystemPadUsed += ShowDisplay;
            closeButton.onClick.AddListener(Close);
        }


        private void ShowDisplay()
        {
            if (actionItemLink == null) return;

            RedrawIndicaorLights();

            uiCanvas.SetActive(true);
        }

        private void RedrawIndicaorLights()
        {
            foreach (Transform child in indicatorLightsPanel)
            {
                Destroy(child.gameObject);
            }

            SystemPad systemPad = (SystemPad)actionItemLink.CurrentActionItem;

            ButtonColor[] buttonColors = systemPad.GetButtonColors();

            for (int i = 0; i < buttonColors.Length; i++)
            {
                var indicatorLight = Instantiate(indicatorLightPrefab, indicatorLightsPanel);
                indicatorLight.GetComponent<Image>().color = GetButtonColor(buttonColors[i]);
            }
        }

        private void HideDisplay()
        {
            uiCanvas.SetActive(false);
        }

        private void Close()
        {
            HideDisplay();
        }


        private Color GetButtonColor(ButtonColor buttonColor)
        {
            Color newButtonColor = Color.black;
            switch (buttonColor)
            {
                case ButtonColor.Black:
                    newButtonColor = Color.black;
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
