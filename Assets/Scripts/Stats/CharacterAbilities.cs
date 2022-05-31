using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class CharacterAbilities : MonoBehaviour
    {
        [SerializeField] CharacterAbility[] characterAbilities;
        [SerializeField] AbilityModifiers abilityModifiers;

        public int GetAbilityModifier(Ability ability)
        {
            int abilityScore = 0;

            foreach (var characterAbility in characterAbilities)
            {
                if (characterAbility.Ability == ability)
                {
                    abilityScore = characterAbility.AbilityValue;
                }
            }
            return abilityModifiers.GetModifier(abilityScore);
        }

        public CharacterAbility[] GetCharacterAbilities()
        {
            return characterAbilities;
        }
    }

    [System.Serializable]
    public class CharacterAbility
    {
        [SerializeField] Ability ability;
        [SerializeField] int abilityValue = 10;

        public Ability Ability { get { return ability; } }
        public int AbilityValue {  get { return abilityValue;  } }

    }


    
}
