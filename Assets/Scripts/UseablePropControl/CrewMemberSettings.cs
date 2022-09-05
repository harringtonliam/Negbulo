using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.Saving;

namespace RPG.UseablePropControl
{

    public class CrewMemberSettings : MonoBehaviour, ISaveable
    {

        [SerializeField] InventoryItem crewMemberItem;

        ButtonColor[] crewMemberColors;

        // Start is called before the first frame update
        void Start()
        {
            if (crewMemberColors == null)
            {
                crewMemberColors = new ButtonColor[5];
                crewMemberColors = GenerateRandomColors();
            }
                
        }

        public bool IsCrewMember(ButtonColor[] buttonColors)
        {
            bool returnValue = true;

            for (int i = 0; i < crewMemberColors.Length; i++)
            {
                if (buttonColors[i] != crewMemberColors[i])
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public bool IsItemCrewMember(InventoryItem sleeper)
        { 
            if(sleeper.ItemID == crewMemberItem.ItemID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        ButtonColor[] GenerateRandomColors()
        {
            ButtonColor[] randomColors = new ButtonColor[5];

            for (int i = 0; i < crewMemberColors.Length; i++)
            {
                randomColors[i] = GenerateRandomColor();
                Debug.Log("Crew member settings  " + i.ToString() + " : " + randomColors[i].ToString());
            }

            //TODO:  these are for testing
            randomColors[0] = ButtonColor.White;
            randomColors[1] = ButtonColor.White;

            return randomColors;
        }

        ButtonColor GenerateRandomColor()
        {
            ButtonColor newColor = ButtonColor.Black;

            int randomInt = Random.Range(0, 9);
            switch(randomInt)
            {
                case 0: newColor = ButtonColor.Blue;
                    break;
                case 1:
                    newColor = ButtonColor.Cyan;
                    break;
                case 2:
                    newColor = ButtonColor.Gray;
                    break;
                case 3:
                    newColor = ButtonColor.Green;
                    break;
                case 4:
                    newColor = ButtonColor.Magenta;
                    break;
                case 5:
                    newColor = ButtonColor.Orange;
                    break;
                case 6:
                    newColor = ButtonColor.Red;
                    break;
                case 7:
                    newColor = ButtonColor.White;
                    break;
                default:
                    newColor = ButtonColor.Yellow;
                    break;

            }


            return newColor;
        }

        public object CaptureState()
        {
            string[] stringColors = new string[5];
            for (int i = 0; i < crewMemberColors.Length; i++)
            {
                stringColors[i] = crewMemberColors[i].ToString();
            }
            return stringColors;
        }

        public void RestoreState(object state)
        {
            crewMemberColors = new ButtonColor[5];
            string[] stringColors = (string[])state;
            for (int i = 0; i < stringColors.Length; i++)
            {
                switch (stringColors[i])
                {
                    case "Blue":
                        crewMemberColors[i] = ButtonColor.Blue;
                        break;
                    case "Cyan":
                        crewMemberColors[i] = ButtonColor.Cyan;
                        break;
                    case "Gray":
                        crewMemberColors[i] = ButtonColor.Gray;
                        break;
                    case "Green":
                        crewMemberColors[i] = ButtonColor.Green;
                        break;
                    case "Magenta":
                        crewMemberColors[i] = ButtonColor.Magenta;
                        break;
                    case "Orange":
                        crewMemberColors[i] = ButtonColor.Orange;
                        break;
                    case "Red":
                        crewMemberColors[i] = ButtonColor.Red;
                        break;
                    case "White":
                        crewMemberColors[i] = ButtonColor.White;
                        break;
                    default:
                        crewMemberColors[i] = ButtonColor.Yellow;
                        break;

                }
            }
        }
    }


}


