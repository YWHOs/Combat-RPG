using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stat/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable;

        public float GetStat(Stats _stat, CharacterClass _characterClass, int _level)
        {
            BuildLookpup();
            float[] levels = lookupTable[_characterClass][_stat];

            if (levels.Length < _level) return 0;

            return levels[_level - 1];
            //foreach (ProgressionCharacterClass progression in characterClasses)
            //{
            //    if (progression.characterClass != _characterClass) continue;

            //    foreach(ProgressionStat progressionStat in progression.stats)
            //    {
            //        if (progressionStat.stat != _stat) continue;

            //        if (progressionStat.levels.Length < _level) continue;

            //        return progressionStat.levels[_level - 1];
            //    }
            //}
            //return 0;
        }

        private void BuildLookpup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();

            foreach (ProgressionCharacterClass progression in characterClasses)
            {
                var statLookupTable = new Dictionary<Stats, float[]>();
                foreach (ProgressionStat progressionStat in progression.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progression.characterClass] = statLookupTable;
            }
        }

        public int GetLevels(Stats _stat, CharacterClass _characterClass)
        {
            BuildLookpup();

            float[] levels = lookupTable[_characterClass][_stat];
            return levels.Length;
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
