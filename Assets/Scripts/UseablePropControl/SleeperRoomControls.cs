using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
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
        [SerializeField] GameObject sleeperCoffinPrefab;
        [SerializeField] Transform sleeperCoffinSpawnPoint;



        ButtonColor[] selectedButtonColors = new ButtonColor[6];


        public ButtonColor[] SelectedButtonColors { get { return selectedButtonColors; } }

        UseablePropLink sleeperRoomControlLink;

        public event Action selectionUpdated;
        public event Action selectionConfirmed;

        private int currentButtonIndex = 0;


        public void SetColorOfCurrentButton(ButtonColor buttonColor)
        {
            SetButtonColor(currentButtonIndex, buttonColor);
            if(currentButtonIndex == selectedButtonColors.Length -1)
            {
                SpawnSleeperCoffin();
                ClearSelectedColors();
            }
            else
            {
                currentButtonIndex++;
            }
            
        }

        private void SpawnSleeperCoffin()
        {
            GameObject sleeperCoffin = GameObject.Instantiate(sleeperCoffinPrefab, sleeperCoffinSpawnPoint.position, Quaternion.identity, this.transform);
            PropMover propMover = GetComponent<PropMover>();
            propMover.SetPropToMove(sleeperCoffin.transform);
            propMover.TriggerMoveToEnd();
        }

        public void SetButtonColor(int index, ButtonColor newColor)
        {
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
