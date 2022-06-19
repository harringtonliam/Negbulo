using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;
using System;

namespace RPG.UseablePropControl
{

    public enum ButtonColor
    {
        White,
        Black,
        Gray,
        Yellow,
        Red,
        Green,
        Blue,
        Cyan,
        Magenta,
        Orange
    }

    public class SleeperRoomControls : UseableProp
    {

        ButtonColor[] selectedButtonColors = new ButtonColor[6];


        public ButtonColor[] SelectedButtonColors { get { return selectedButtonColors; } }

        UseablePropLink sleeperRoomControlLink;

        public event Action selectionUpdated;
        public event Action selectionConfirmed;

        private int currentButtonIndex = 0;


        public void SetColorOfCurrentButton(ButtonColor buttonColor)
        {
            SetButtonColor(currentButtonIndex, buttonColor);
            currentButtonIndex++;
        }

        public void SetButtonColor(int index, ButtonColor newColor)
        {
            Debug.Log("SleeperRoomControls SetButtonColor " + index.ToString() + "  " + newColor.ToString());
            selectedButtonColors[index] = newColor;
            CallEventSelectionUpdated();
        }

        public void ClearSelectedColors()
        {
            selectedButtonColors = new ButtonColor[6];
            CallEventSelectionUpdated();
        }

        public void ConfirmSelection()
        {
            Debug.Log("Selection Confirmed " + selectedButtonColors.ToString());
            if (selectionConfirmed != null)
            {
                selectionConfirmed();
            }
        }

        private void CallEventSelectionUpdated()
        {
            if (selectionUpdated != null)
            {
                selectionUpdated();
            }
        }
    }

}
