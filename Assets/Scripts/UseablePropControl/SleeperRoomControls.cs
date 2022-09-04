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
        Black,
        White,
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
        [SerializeField] GameObject[] sleeperCoffinWallPanels;
        [SerializeField] Transform[] sleeperCoffinSpawnPoints;
        [SerializeField] Transform[] sleeperCoffinEndPoints;
        [SerializeField] ButtonColor[] defaultButtonColors = new ButtonColor[2];

        ButtonColor[] selectedButtonColors = new ButtonColor[5];


        public ButtonColor[] SelectedButtonColors { get { return selectedButtonColors; } }

        public event Action selectionUpdated;
        public event Action selectionConfirmed;

        private int currentButtonIndex = 0;
        private GameObject sleeperCoffin = null;

        PropMover propMover = null;
        int randomCoffin = 0;


        private void Start()
        {
            useablePropLink = FindObjectOfType<UseablePropLink>();
            gameConsole = FindObjectOfType<GameConsole>();
            ResetSelectedColors();
        }

        public void SetColorOfCurrentButton(ButtonColor buttonColor)
        {
            if (sleeperCoffin != null) return;

            SetButtonColor(currentButtonIndex, buttonColor);
            if(currentButtonIndex == selectedButtonColors.Length -1)
            {
                SpawnSleeperCoffin();
            }
            else
            {
                currentButtonIndex++;
            }
        }

        public void SetButtonColor(int index, ButtonColor newColor)
        {
            selectedButtonColors[index] = newColor;
            CallEventSelectionUpdated();
        }

        public void ResetControls()
        {

            if (sleeperCoffin == null)
            {
                ResetSelectedColors();
            }
            else if (propMover == null)
            {
                ReferencePropMover();
            }
            propMover.SetPropToMove(sleeperCoffin.transform);
            sleeperCoffin.GetComponent<SleeperCoffinControl>().DisplayCover(true);
            propMover.StartPosition = sleeperCoffinSpawnPoints[randomCoffin];
            propMover.TriggerMoveToStart();
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

        private void DestroySleeperCoffin()
        {
            Destroy(sleeperCoffin, 1f);
        }

        private void ReferencePropMover()
        {
            propMover = GetComponent<PropMover>();
            propMover.reachedStartPoint += CoffinHasMovedBack;
            propMover.reachedEndPoint += CoffinHasMovedOut;
        }

        private void SpawnSleeperCoffin()
        {
            if (propMover == null)
            {
                ReferencePropMover();
            }
            randomCoffin = 0;// UnityEngine.Random.Range(0, sleeperCoffinSpawnPoints.Length);

            Vector3 spawnPositon = sleeperCoffinSpawnPoints[randomCoffin].position;

            sleeperCoffin = GameObject.Instantiate(sleeperCoffinPrefab, spawnPositon, sleeperCoffinSpawnPoints[randomCoffin].rotation, this.transform);
            sleeperCoffinWallPanels[randomCoffin].SetActive(false);
            sleeperCoffin.GetComponent<SleeperCoffinControl>().SleeperCoffinSetup(selectedButtonColors);
            propMover.SetPropToMove(sleeperCoffin.transform);
            propMover.EndPosition = sleeperCoffinEndPoints[randomCoffin];
            propMover.TriggerMoveToEnd();
        }


        private void ResetSelectedColors()
        {
            selectedButtonColors = new ButtonColor[5];
            currentButtonIndex = 0;
            for (int i = 0; i < defaultButtonColors.Length; i++)
            {
                selectedButtonColors[i] = defaultButtonColors[i];
                currentButtonIndex++;
            }
            CallEventSelectionUpdated();
        }

        private void CoffinHasMovedOut()
        {
            ResetSelectedColors();
            sleeperCoffin.GetComponent<SleeperCoffinControl>().DisplayCover(false);
        }

        private void CoffinHasMovedBack()
        {
            sleeperCoffinWallPanels[randomCoffin].SetActive(true);
            DestroySleeperCoffin();
        }
    }

}
