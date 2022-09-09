using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UseablePropControl;
using System;

namespace RPG.InventoryControl
{
    [CreateAssetMenu(menuName = ("Items/SystemPad"))]
    public class SystemPad : ActionItem
    {


        public override void Use(GameObject user)
        {
            WriteToGameConsole();
            DisplaySytemPadUI();

        }

        public ButtonColor[] GetButtonColors()
        {
            CrewMemberSettings crewMemberSettings = FindObjectOfType<CrewMemberSettings>();
            return crewMemberSettings.CrewMemberColors;
        }

        private void DisplaySytemPadUI()
        {
            ActionItemLink actionItemLink = FindObjectOfType<ActionItemLink>();
            if (actionItemLink != null)
            {
                actionItemLink.DisplayActionItemUI(this);
            }
        }

        private void WriteToGameConsole()
        {
            CrewMemberSettings crewMemberSettings = FindObjectOfType<CrewMemberSettings>();
            System.Text.StringBuilder consoleText = new System.Text.StringBuilder();
            consoleText.Append("When you look at the pad it simply displays a row of indicators: ");

            foreach (var item in crewMemberSettings.CrewMemberColors)
            {
                consoleText.Append(item.ToString());
                consoleText.Append(" ");
            }

            WriteToConsole(consoleText.ToString());
        }

        private void WriteToConsole(string textToWrite)
        {
            GameConsole gameConsole = FindObjectOfType<GameConsole>();
            if (gameConsole == null) return;

            gameConsole.AddNewLine(textToWrite);

        }
    }

}


