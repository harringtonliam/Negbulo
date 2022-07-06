using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;

namespace RPG.UseablePropControl
{
    public class SleeperCoffinControl : MonoBehaviour
    {

        [SerializeField] ButtonColor[] buttonColors = new ButtonColor[5];
        [SerializeField] GameObject[] indicatorLight = new GameObject[5];
        [SerializeField] Material sleeperCoffinBlack;
        [SerializeField] Material sleeperCoffinWhite;
        [SerializeField] Material sleeperCoffinGray;
        [SerializeField] Material sleeperCoffinGreen;
        [SerializeField] Material sleeperCoffinBlue;
        [SerializeField] Material sleeperCoffinYellow;
        [SerializeField] Material sleeperCoffinCyan;
        [SerializeField] Material sleeperCoffinMagenta;
        [SerializeField] Material sleeperCoffinRed;
        [SerializeField] Material sleeperCoffinOrange;


        public ButtonColor[] ButtonColors { get { return buttonColors; } }

        SleeperCoffinRegister sleeperCoffinRegister = null;
        Inventory sleeperCoffinInventory = null;

        private void Start()
        {
            sleeperCoffinRegister = FindObjectOfType<SleeperCoffinRegister>();
            sleeperCoffinInventory = GetComponent<Inventory>();
        }


        public void SleeperCoffinSetup(ButtonColor[] buttonColors)
        {
            SetButtonColors(buttonColors);
            SetCoffinContents(buttonColors);
        }

        private void SetCoffinContents(ButtonColor[] buttonColors)
        {
            sleeperCoffinRegister = FindObjectOfType<SleeperCoffinRegister>();

            InventoryItem inventoryItem = sleeperCoffinRegister.GetCoffinContents(buttonColors);
            Inventory inventory = GetComponent<Inventory>();
            inventory.RemoveFromSlot(0, 1);
            inventory.AddItemToSlot(0, inventoryItem, 1);
        }

        private void SetButtonColors(ButtonColor[] buttonColors)
        {
            this.buttonColors = buttonColors;
            for (int i = 0; i < this.buttonColors.Length; i++)
            {
                indicatorLight[i].GetComponent<MeshRenderer>().material = SelectMaterial(buttonColors[i]);
            }

        }


        private Material SelectMaterial(ButtonColor buttonColor)
        {

            Material selectedMaterial = sleeperCoffinWhite;
            switch (buttonColor)
            {
                case ButtonColor.Black:
                    selectedMaterial = sleeperCoffinBlack;
                    break;
                case ButtonColor.White:
                    selectedMaterial = sleeperCoffinWhite;
                    break;
                case ButtonColor.Gray:
                    selectedMaterial = sleeperCoffinGray;
                    break;
                case ButtonColor.Green:
                    selectedMaterial = sleeperCoffinGreen;
                    break;
                case ButtonColor.Blue:
                    selectedMaterial = sleeperCoffinBlue;
                    break;
                case ButtonColor.Yellow:
                    selectedMaterial = sleeperCoffinYellow;
                    break;
                case ButtonColor.Cyan:
                    selectedMaterial = sleeperCoffinCyan;
                    break;
                case ButtonColor.Magenta:
                    selectedMaterial = sleeperCoffinMagenta;
                    break;
                case ButtonColor.Red:
                    selectedMaterial = sleeperCoffinRed;
                    break;
                case ButtonColor.Orange:
                    selectedMaterial = sleeperCoffinOrange;
                    break;
                default:
                    selectedMaterial = sleeperCoffinWhite;
                    break;

            }
            return selectedMaterial;
        }


    }

}
