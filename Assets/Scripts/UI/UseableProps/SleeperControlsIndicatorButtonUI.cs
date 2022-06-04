using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UseablePropControl;
using UnityEngine.UI;

namespace RPG.UI.UseableProps
{
    public class SleeperControlsIndicatorButtonUI : MonoBehaviour
    {
        [SerializeField] ButtonColor buttonColor;

        Button controlButton;

        // Start is called before the first frame update
        void Start()
        {
            controlButton = GetComponent<Button>();
            controlButton.onClick.AddListener(Button_onClick);
        }


        private void Button_onClick()
        {
            Debug.Log("Button Clicked " + buttonColor.ToString());
        }
    }
}


