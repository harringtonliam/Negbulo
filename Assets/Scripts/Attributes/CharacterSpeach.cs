using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{

    public class CharacterSpeach : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI speachTextUI;
        [SerializeField] float timeToDisplaySpeachFor;
        [SerializeField] string[] speachText;
        [SerializeField] bool displaySpeachTrigger = true;
        [SerializeField] Canvas rootCanvas = null;

        int speachTextIndex = 0;



        public string[] SpeachText
        {
            get { return speachText; }
            set { speachText = value; }
        }

        private void Start()
        {
            if (displaySpeachTrigger)
            {
                TriggerSpeach(displaySpeachTrigger);
            }
        }

        public void TriggerSpeach(bool displaySpeach)
        {
            if (displaySpeach && speachText.Length >0)
            {
                rootCanvas.enabled = true;
                StartCoroutine(DisplaySpeach());
            }
            else
            {
                rootCanvas.enabled = false;
                StopCoroutine(DisplaySpeach());
            }

        }



        private IEnumerator DisplaySpeach()
        {
            Debug.Log("Display Speach");
            if (speachTextIndex >= speachText.Length)
            {
                speachTextIndex = 0;
            }
            speachTextUI.text = speachText[speachTextIndex];
            speachTextIndex++;
            yield return new WaitForSeconds(timeToDisplaySpeachFor);
        }
    }

}


