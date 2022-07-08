using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.InventoryControl
{
    [CreateAssetMenu(menuName = ("Items/Sleeper"))]
    public class Sleeper : InventoryItem
    {
        [SerializeField] GameObject characterPrefabToSpawn;
     
        public GameObject CharacterPrefabToSpawn { get { return characterPrefabToSpawn; } }



    }

}
