using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stat/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetHealth(CharacterClass _characterClass, int _level)
        {
            foreach(ProgressionCharacterClass progression in characterClasses)
            {
                if(progression.characterClass == _characterClass)
                {
                    return progression.health[_level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }

}
