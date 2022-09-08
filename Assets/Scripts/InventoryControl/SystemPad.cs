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

        CrewMemberSettings crewMemberSettings;

        public event Action systemPadUse;

        public override void Use(GameObject user)
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

            systemPadUse();

        }

        private void WriteToConsole(string textToWrite)
        {
            GameConsole gameConsole = FindObjectOfType<GameConsole>();
            if (gameConsole == null) return;

            gameConsole.AddNewLine(textToWrite);

        }
    }

}


