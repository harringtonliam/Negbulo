using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG.Control;
using RPG.Core;
using RPG.Saving;

namespace RPG.UseablePropControl
{

    public class UseableProp : MonoBehaviour, IRaycastable, ISaveable
    {
        [SerializeField] protected bool isActivated = false;
        [SerializeField] UnityEvent<bool> useProp;
        [SerializeField] UnityEvent<float> usePropFloat;
        [SerializeField] UnityEvent<PatrolPath> usePropPatrolPath;
        [SerializeField] UnityEvent<GameObject> usePropCombatTarget;
        [TextArea]
        [SerializeField] string displayText = "This thing does stuff";
        [TextArea]
        [SerializeField] string activateText = "Click to activate this item";
        [TextArea]
        [SerializeField] string deactivateText = "Click to deactivate this item";
        [TextArea]
        [SerializeField] protected string onActivatedText = "Item activated";
        [TextArea]
        [SerializeField] protected string onDeactivatedText = "Item deactivated";
        [SerializeField] float deactivatedValue = 5f;
        [SerializeField] float activatedValue = 10f;
        [SerializeField] PatrolPath deactivatedPatrolPath;
        [SerializeField] PatrolPath activatedPatrolPath;
        [SerializeField] GameObject deactivatedCombatTarget;
        [SerializeField] GameObject activatedCombatTaregt;
        [SerializeField] protected AudioSource activateSound;
        [SerializeField] protected AudioSource deactivateSound;
        [SerializeField] protected bool isDisabled = false;
        [SerializeField] protected string isDisabledText = "This thing has been disabled.";

        public string DisplayText
        {
            get { return displayText; }
        }

        public string ActivateText
        {
            get { return activateText; }
        }

        public string DeactivateText
        {
            get { return deactivateText; }
        }

        public bool IsDisabled { get { return isDisabled; } }
        public string IsDisabledText {  get { return isDisabledText; } }


        protected UseablePropLink useablePropLink;
        protected GameConsole gameConsole;

        private void Start()
        {
            useablePropLink = FindObjectOfType<UseablePropLink>();
            gameConsole = FindObjectOfType<GameConsole>();
        }

        public CursorType GetCursorType()
        {
            return CursorType.Use;
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UseProp useProp = playerController.transform.GetComponent<UseProp>();
                if (useProp != null)
                {
                    useProp.StartUseProp(gameObject);
                }
            }
            return true;
        }

        public void UseProp()
        {
            if(useablePropLink != null)
            {
                useablePropLink.DisplayUsePropUI(this);
            }
        }

        public virtual void SetPropActivatedStatus(bool activatedStatus)
        {
            isActivated = activatedStatus;
            useProp.Invoke(isActivated);
            if (isActivated)
            {
                usePropFloat.Invoke(activatedValue);
                usePropPatrolPath.Invoke(activatedPatrolPath);
                usePropCombatTarget.Invoke(activatedCombatTaregt);
                WriteToConsole(onActivatedText);
            }
            else
            {
                usePropFloat.Invoke(deactivatedValue);
                usePropPatrolPath.Invoke(deactivatedPatrolPath);
                usePropCombatTarget.Invoke(deactivatedCombatTarget);
                WriteToConsole(onDeactivatedText);
            }

            if (isActivated && activateSound != null)
            {
                activateSound.Play();
            }
            else if (!isActivated && deactivateSound != null)
            {
                deactivateSound.Play();
            }

        }

        protected void WriteToConsole(string textToWrite)
        {
            if (gameConsole == null) return;

            gameConsole.AddNewLine(textToWrite.Replace("\n", " "));
        }



        [System.Serializable]
        public struct UseAblePropSaveData
        {
            public bool isActivated;
            public bool isDisabled;
        }

        public object CaptureState()
        {
            UseAblePropSaveData useAblePropSaveData = new UseAblePropSaveData();
            useAblePropSaveData.isActivated = this.isActivated;
            useAblePropSaveData.isDisabled = this.isDisabled;
            return useAblePropSaveData;
        }

        public void RestoreState(object state)
        {
            UseAblePropSaveData useAblePropSaveData = (UseAblePropSaveData)state;
            isActivated = useAblePropSaveData.isActivated;
            isDisabled = useAblePropSaveData.isDisabled;

        }
    }
}

