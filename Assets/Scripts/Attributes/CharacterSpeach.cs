using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{

    public class CharacterSpeach : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI speachTextUI;
        [SerializeField] float timeToDisplaySpeachFor = 1.5f;
        [SerializeField] string[] speachText;
        [SerializeField] bool displaySpeachTrigger = true;
        [SerializeField] Canvas rootCanvas = null;


        public string[] SpeachText
        {
            get { return speachText; }
            set { speachText = value; }
        }

        private void Start()
        {
            if (displaySpeachTrigger)
            {
                StartCoroutine(DisplaySpeach());
            }
        }

        public void TriggerSpeach(bool displaySpeach)
        {
            StartCoroutine(DisplaySpeach());
        }



        private IEnumerator DisplaySpeach()
        {
            for (int i = 0; i < speachText.Length; i++)
            {
                rootCanvas.enabled = true;
                speachTextUI.text = speachText[i];
                yield return new WaitForSeconds(timeToDisplaySpeachFor);
            }

            rootCanvas.enabled = false;

        }
    }

}


