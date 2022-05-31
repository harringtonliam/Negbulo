using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Attributes;
using System;

namespace RPG.DialogueControl
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        string conversantName;
        Sprite conversantPortrait;


        public string ConversantName
        {
            get { return conversantName; }
        }

        public Sprite ConversantPortrait
        {
            get { return conversantPortrait; }
        }

        public Dialogue Dialogue
        {
            get { return dialogue; }
        }

        private void Start()
        {
            CharacterSheet characterSheet = GetComponent<CharacterSheet>();
            if (characterSheet == null) 
            {
                conversantName = "unknown";
                conversantPortrait = null;
            }
            else
            {
                conversantName = characterSheet.CharacterName;
                conversantPortrait = characterSheet.Portrait;
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Dialog;
        }

     
        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if(IsHostile())
            {
                return false; ;
            }

            if (Input.GetMouseButtonDown(0))
            {

                PlayerConversant playerConversant = callingController.GetComponent<PlayerConversant>();
                playerConversant.StartDialogue(this, dialogue);
   
            }
            return true;
        }

        private bool IsHostile()
        {
            AIControler aIControler = GetComponent<AIControler>();
            if (aIControler != null)
            {
                if (aIControler.AIRelationship == AIRelationship.Hostile)
                {
                    return true;
                }
            }

            return false;
        }
    }

}


