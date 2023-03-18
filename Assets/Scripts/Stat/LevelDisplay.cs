using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStat baseStat;
        void Awake()
        {
            baseStat = GameObject.FindWithTag("Player").GetComponent<BaseStat>();
        }

        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = baseStat.GetLevel().ToString();
        }
    }

}
