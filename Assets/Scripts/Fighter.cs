using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float time = 1f;
        [SerializeField] float weaponDamage = 15f;
        Transform target;
        float lastTime = 0f;
        void Update()
        {
            lastTime += Time.deltaTime;
            if (target == null) return;
            if (target != null && !IsRange())
            {
                GetComponent<Move>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackAnimator();
            }
        }

        private void AttackAnimator()
        {
            if(lastTime > time)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                lastTime = 0f;
            }
        }
        // Animation Event
        void Hit()
        {
            Health health = target.GetComponent<Health>();
            health.TakeDamage(weaponDamage);
        }
        private bool IsRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget _target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = _target.transform;
        }
        public void Cancel()
        {
            target = null;
        }
    }
}

