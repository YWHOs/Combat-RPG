using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float time = 1f;

        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;

        Health target;
        Weapon weapon;
        float lastTime = Mathf.Infinity;
        void Start()
        {
            if(weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
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

        public Health GetTarget()
        {
            return target;
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

            float damage = GetComponent<BaseStat>().GetStat(Stats.Damage);
            if (weapon.HasProjectile())
            {
                weapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
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
        public IEnumerable<float> GetAddModifier(Stats _stat)
        {
            if(_stat == Stats.Damage)
            {
                yield return weapon.WeaponDamage;
            }
        }
        public object CaptureState()
        {
            return weapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}

