using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SceneCharacters : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject crewMemberPrefab;
        [SerializeField] Vector3 crewMemberPosition;
        [SerializeField] bool crewMemberPresent;


        [System.Serializable]
        public struct SaveCharacterSlot
        {
            public bool crewMemberPresent;
            public SerializableVector3 position;
        }


        public void WakeCrewMember(Vector3 poaition)
        {
            crewMemberPosition = poaition;
            crewMemberPresent = true;
        }

        public object CaptureState()
        {
            SaveCharacterSlot saveCharacterSlot = new SaveCharacterSlot();
            saveCharacterSlot.crewMemberPresent = crewMemberPresent;
            saveCharacterSlot.position = new SerializableVector3(crewMemberPosition);

            return saveCharacterSlot;
        }

        public void RestoreState(object state)
        {
            SaveCharacterSlot saveCharacterSlot = (SaveCharacterSlot) state;
            crewMemberPresent = saveCharacterSlot.crewMemberPresent;
            crewMemberPosition = saveCharacterSlot.position.ToVector();
            if (crewMemberPresent && crewMemberPrefab != null)
            {
                GameObject newCrewMember = Instantiate(crewMemberPrefab, crewMemberPosition, Quaternion.identity);
                newCrewMember.transform.parent = this.transform;
                SaveableEntity saveableEntity = newCrewMember.GetComponent<SaveableEntity>();
                Debug.Log("Scene Characters REstore state crewmember savabale entity uid= " + saveableEntity.GetUniqueIdentifier());
                if (saveableEntity != null)
                {
                    FindObjectOfType<SavingWrapper>().LoadSaveableEntity(saveableEntity);
                }
            }
        }
    }

}


