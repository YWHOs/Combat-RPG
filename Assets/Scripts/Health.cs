using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stat;
using System;
using RPG.Combat;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        float health = -1f;

        bool isDead;
        public bool IsDead() { return isDead; }

        void Start()
        {
            health = GetComponent<BaseStat>().GetStat(Stats.Health);
        }

        public void TakeDamage(GameObject _instigator, float _damage)
        {
            health = Mathf.Max(health - _damage, 0);
            if(health == 0)
            {
                Die();
                AwardExp(_instigator);
            }
        }
        public float PercentageHealth()
        {
            return 100 * (health / GetComponent<BaseStat>().GetStat(Stats.Health));
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelAction();
        }
        private void AwardExp(GameObject _instigator)
        {
            Experience exp = _instigator.GetComponent<Experience>();
            if (exp == null) return;

            exp.GetExp(GetComponent<BaseStat>().GetStat(Stats.EXP));
        }
        public object CaptureState()
        {
            return health;
        }
        public void RestoreState(object state)
        {
            health = (float)state;

            if (health == 0)
            {
                Die();
            }
        }
    }
}

