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

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, level);
        }

        public float GetEXP()
        {
            return 10;
        }
    }

}
