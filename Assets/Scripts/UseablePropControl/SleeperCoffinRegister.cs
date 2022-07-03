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

        Dictionary<ButtonColor[], InventoryItem> sleeperCoffins;

        // Start is called before the first frame update
        void Start()
        {
            if (sleeperCoffins == null)
            {
                sleeperCoffins = new Dictionary<ButtonColor[], InventoryItem>();
            }
        }

        public InventoryItem  GetCoffinContents(ButtonColor[] buttonColors)
        {

            Debug.Log("GetCoffinContents " + buttonColors[2].ToString());
            if (!sleeperCoffins.ContainsKey(buttonColors))
            {
                sleeperCoffins.Add(buttonColors, GenerateRandomSleeper());
                Debug.Log("slepperconffins size =  " + sleeperCoffins.Count);

            }
            return sleeperCoffins[buttonColors];
        }

        private InventoryItem GenerateRandomSleeper()
        {
            int randomIndex = Random.Range(0, availableSleepers.Length);
            Debug.Log("GenerateRandomSleeper " + randomIndex.ToString());
            return availableSleepers[randomIndex];
        }


        [System.Serializable]
        private struct SleeperCoffinRecord
        {
            public ButtonColor[] buttonColors;
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
                sleeperCoffins = new Dictionary<ButtonColor[], InventoryItem>();
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

