using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        void Update()
        {
            if (target == null) return;
            if (target != null && !IsRange())
            {
                GetComponent<Move>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Move>().Stop();
            }
        }

        private bool IsRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget _target)
        {
            target = _target.transform;
        }
        public void Cancel()
        {
            target = null;
        }
    }
}

