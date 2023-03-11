using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        bool isDead;
        public bool IsDead() { return isDead; }

        public void TakeDamage(float _damage)
        {
            health = Mathf.Max(health - _damage, 0);
            if(health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelAction();
        }
    }
}

