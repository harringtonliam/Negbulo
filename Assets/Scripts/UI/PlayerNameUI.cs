using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.UI
{
    public class PlayerNameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText = null;

        GameObject playerCharacterGameObject = null;


        void Start()
        {
            playerCharacterGameObject = GameObject.FindGameObjectWithTag("Player");
            RedrawUI();
        }

        public void RedrawUI()
        {
            CharacterSheet characterSheet = playerCharacterGameObject.GetComponent<CharacterSheet>();
            if (characterSheet != null)
            {
                nameText.text = characterSheet.CharacterName; ;
            }

        }


    }

}



