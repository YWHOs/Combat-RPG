using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stat;
using System;
using RPG.Combat;
using RPG.Utils;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regeneratePercent = 70f;
        LazyValue<float> health;

        bool isDead;
        public bool IsDead() { return isDead; }

        void Awake()
        {
            health = new LazyValue<float>(GetInitialHealth);
        }
        float GetInitialHealth()
        {
            return GetComponent<BaseStat>().GetStat(Stats.Health);
        }
        void Start()
        {
            health.ForceInit();
            //if (health < 0)
            //{
            //    health = GetComponent<BaseStat>().GetStat(Stats.Health);
            //}
            
        }
        void OnEnable()
        {
            GetComponent<BaseStat>().onLevelUp += RegenerateHealth;
        }
        void OnDisable()
        {
            GetComponent<BaseStat>().onLevelUp -= RegenerateHealth;
        }


        public void TakeDamage(GameObject _instigator, float _damage)
        {
            health.value = Mathf.Max(health.value - _damage, 0);
            if(health.value == 0)
            {
                Die();
                AwardExp(_instigator);
            }
        }

        public float GetHealth()
        {
            return health.value;
        }
        public float GetMaxHealth()
        {
            return GetComponent<BaseStat>().GetStat(Stats.Health);
        }
        public float PercentageHealth()
        {
            return 100 * (health.value / GetComponent<BaseStat>().GetStat(Stats.Health));
        }
        private void RegenerateHealth()
        {
            float regenHealth = GetComponent<BaseStat>().GetStat(Stats.Health) * (regeneratePercent / 100);
            health.value = Mathf.Max(health.value, regenHealth);
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
            return health.value;
        }
        public void RestoreState(object state)
        {
            health.value = (float)state;

            if (health.value == 0)
            {
                Die();
            }
        }
    }
}

