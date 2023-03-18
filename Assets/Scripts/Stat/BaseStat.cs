using RPG.Combat;
using System;
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
        [SerializeField] GameObject levelUpEffect;

        public event Action onLevelUp;

        int currentLevel = 0;

        void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGain += UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpEffect, transform);
        }

        public float GetStat(Stats _stat)
        {
            return progression.GetStat(_stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
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
