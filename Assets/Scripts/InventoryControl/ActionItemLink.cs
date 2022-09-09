using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace RPG.InventoryControl
{
    public class ActionItemLink : MonoBehaviour
    {

        public event Action onActionItemUsed;
        public event Action onSystemPadUsed;


        ActionItem currentActionItem;

        public ActionItem CurrentActionItem {  get { return currentActionItem; } }


        public void DisplayActionItemUI(ActionItem actionItem)
        {
            currentActionItem = actionItem;
            if (actionItem.GetType() == typeof(SystemPad))
            {
                if (onSystemPadUsed != null)
                {
                    onSystemPadUsed();
                }
                
            }
            else
            {
                if (onActionItemUsed != null)
                {
                    onActionItemUsed();
                }

            }
        }
    }

}

