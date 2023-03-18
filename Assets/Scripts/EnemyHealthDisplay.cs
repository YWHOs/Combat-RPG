using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<UnityEngine.UI.Text>().text = "N/A";
            }
            Health health = fighter.GetTarget();
            GetComponent<UnityEngine.UI.Text>().text = health.PercentageHealth() + "%";
        }
    }
}

