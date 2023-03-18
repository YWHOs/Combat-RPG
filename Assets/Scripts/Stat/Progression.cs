using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stat/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetStat(Stats _stat, CharacterClass _characterClass, int _level)
        {
            foreach (ProgressionCharacterClass progression in characterClasses)
            {
                if (progression.characterClass != _characterClass) continue;

                foreach(ProgressionStat progressionStat in progression.stats)
                {
                    if (progressionStat.stat != _stat) continue;

                    if (progressionStat.levels.Length < _level) continue;

                    return progressionStat.levels[_level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stats stat;
            public float[] levels;
        }
         
    }
}
