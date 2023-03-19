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
        [SerializeField] bool isUseModifier;

        public event Action onLevelUp;

        int currentLevel = 0;
        Experience experience;

        void Awake()
        {
            experience = GetComponent<Experience>();
        }
        void Start()
        {
            currentLevel = CalculateLevel();
        }
        void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGain += UpdateLevel;
            }
        }
        void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGain -= UpdateLevel;
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
            return (GetBaseStat(_stat) + GetAddModifier(_stat)) * (1 + GetPercentModifier(_stat) / 100);
        }

        float GetBaseStat(Stats _stat)
        {
            return progression.GetStat(_stat, characterClass, GetLevel());
        }

        float GetAddModifier(Stats _stat)
        {
            if (!isUseModifier) return 0;
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modify in provider.GetAddModifier(_stat))
                {
                    total += modify;
                }
            }
            return total;
        }
        float GetPercentModifier(Stats _stat)
        {
            if (!isUseModifier) return 0;
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modify in provider.GetPercentModifier(_stat))
                {
                    total += modify;
                }
            }
            return total;
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
