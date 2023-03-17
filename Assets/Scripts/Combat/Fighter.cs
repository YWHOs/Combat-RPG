using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float time = 1f;

        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;
        [SerializeField] string defaultWeaponName = "Unarmed";


        Health target;
        Weapon weapon;
        float lastTime = Mathf.Infinity;
        void Start()
        {
            Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            EquipWeapon(weapon);
        }
        void Update()
        {
            lastTime += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (target != null && !IsRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackAnimator();
            }
        }
        public void EquipWeapon(Weapon _weapon)
        {
            weapon = _weapon;
            Animator animator = GetComponent<Animator>();
            _weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }
        void AttackAnimator()
        {
            transform.LookAt(target.transform);
            if (lastTime > time)
            {
                AttackTrigger();
                lastTime = 0f;
            }
        }

        void AttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;

            if (weapon.HasProjectile())
            {
                weapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(weapon.WeaponDamage);
            }
        }
        void Shoot()
        {
            Hit();
        }
        bool IsRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.WeaponRange;
        }

        public void Attack(GameObject _target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = _target.GetComponent<Health>();
        }
        public bool CanAttack(GameObject _target)
        {
            if(_target == null) { return false; }
            Health health = _target.GetComponent<Health>();
            return health != null && !health.IsDead();
        }
        public void Cancel()
        {
            StopAttackTrigger();
            target = null;
            GetComponent<Move>().Cancel();
        }

        void StopAttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}

