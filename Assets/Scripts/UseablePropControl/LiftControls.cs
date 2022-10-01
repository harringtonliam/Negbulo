using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using System;

namespace RPG.UseablePropControl
{
    public class LiftControls : UseableProp
    {
        [SerializeField] Portal portal;
        [SerializeField] ButtonSceneLink[] buttonSceneLinks;


        [Serializable]
        public struct ButtonSceneLink
        {
            public ButtonColor buttonColor;
            public string scene;
        }


        public void ButtonPressed(ButtonColor buttonColor)
        {
            if (portal == null) return;
            foreach (var buttonSceneLink in buttonSceneLinks)
            {
                if (buttonSceneLink.buttonColor == buttonColor)
                {
                    portal.SetSceneToLoad(buttonSceneLink.scene);
                    Debug.Log("Portal scene to load = " + buttonSceneLink.scene);
                    return;
                }
            }


        }
    }
}

