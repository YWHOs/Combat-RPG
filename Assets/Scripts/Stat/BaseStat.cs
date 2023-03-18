using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    public class BaseStat : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int level = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;

        void Update()
        {
            if (gameObject.CompareTag("Player"))
            {
                
            }
        }
        public float GetStat(Stats _stat)
        {
            return progression.GetStat(_stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return level;

            float currentEXP = experience.GetPoint();
            int maxLevel = progression.GetLevels(Stats.EXPToLevelUp, characterClass);
            for (int i = 1; i <= maxLevel; i++)
            {
                float EXPLevelUp = progression.GetStat(Stats.EXPToLevelUp, characterClass, i);
                if(EXPLevelUp > currentEXP)
                {
                    return i;
                }
            }
            return maxLevel + 1;
        }
    }

}
