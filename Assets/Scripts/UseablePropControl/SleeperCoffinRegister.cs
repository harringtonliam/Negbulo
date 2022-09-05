using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.InventoryControl;

namespace RPG.UseablePropControl
{
    public class SleeperCoffinRegister : MonoBehaviour, ISaveable
    {
        [SerializeField] InventoryItem[] availableSleepers = new InventoryItem[6];
        [SerializeField] InventoryItem crewMember;

        Dictionary<string, InventoryItem> sleeperCoffins;

        CrewMemberSettings crewMemberSettings;

        // Start is called before the first frame update
        void Start()
        {
            if (sleeperCoffins == null)
            {
                sleeperCoffins = new Dictionary<string, InventoryItem>();
            }
            if(crewMemberSettings == null)
            {
                crewMemberSettings = FindObjectOfType<CrewMemberSettings>();
            }
        }

        public InventoryItem GetCoffinContents(ButtonColor[] buttonColors)
        {

            string buttonColorsString = ConvertButtonArryToString(buttonColors);
            if (!sleeperCoffins.ContainsKey(buttonColorsString))
            {
                if (crewMemberSettings.IsCrewMember(buttonColors))
                {
                    sleeperCoffins.Add(buttonColorsString, crewMember);
                }
                else
                {
                    sleeperCoffins.Add(buttonColorsString, GenerateRandomSleeper());
                }

            }
            return sleeperCoffins[buttonColorsString];
        }

        public void SetCoffinContents(ButtonColor[] buttonColors, InventoryItem inventoryItem)
        {
            string buttonColorsString = ConvertButtonArryToString(buttonColors);
            if (sleeperCoffins.ContainsKey(buttonColorsString))
            {
                sleeperCoffins[buttonColorsString] = inventoryItem;
            }
        }

        private string ConvertButtonArryToString(ButtonColor[] buttonColors)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < buttonColors.Length; i++)
            {
                sb.Append(buttonColors[i].ToString());
                sb.Append(":");
            }
            return sb.ToString();
        }

        private InventoryItem GenerateRandomSleeper()
        {
            int randomIndex = Random.Range(0, availableSleepers.Length);
            return availableSleepers[randomIndex];
        }


        [System.Serializable]
        private struct SleeperCoffinRecord
        {
            public string buttonColors;
            public string itemID;
        }

        public object CaptureState()
        {
            var records = new SleeperCoffinRecord[sleeperCoffins.Count];
            int i = 0;
            foreach (var sleeperCoffin in sleeperCoffins)
            {
                records[i].buttonColors = sleeperCoffin.Key;
                records[i].itemID = sleeperCoffin.Value.ItemID;
                i++;
            }
            return records;
        }

        public void RestoreState(object state)
        {
            if (sleeperCoffins == null)
            {
                sleeperCoffins = new Dictionary<string, InventoryItem>();
            }
            else
            {
                sleeperCoffins.Clear();
            }

            var records = (SleeperCoffinRecord[])state;
            for (int i = 0; i < records.Length; i++)
            {
                sleeperCoffins.Add(records[i].buttonColors, InventoryItem.GetFromID(records[i].itemID));
            }

        }
    }

}

