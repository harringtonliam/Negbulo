using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UseablePropControl;
using UnityEngine.UI;


namespace RPG.UI.UseableProps
{
    public class LiftControlsButtonUI : MonoBehaviour
    {

        [SerializeField] ButtonColor buttonColor;
        [SerializeField] LiftControlsUI liftControlsUI;

        Button controlButton;
        UseablePropLink useablePropLink;


        // Start is called before the first frame update
        void Start()
        {
            controlButton = GetComponent<Button>();
            controlButton.onClick.AddListener(Button_onClick);
            useablePropLink = FindObjectOfType<UseablePropLink>();
        }

        private void Button_onClick()
        {
            if (useablePropLink == null) return;

            LiftControls liftControls = (LiftControls)useablePropLink.CurrentUsableProp;
            liftControls.ButtonPressed(buttonColor);

            if (liftControlsUI != null)
            {
                liftControlsUI.Close();
            }
        }
    }
}
