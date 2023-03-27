using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Stat;
using RPG.Utils;
using System;
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
        [SerializeField] WeaponConfig defaultWeapon;

        Health target;
        //Weapon weapon;
        WeaponConfig weaponConfig;
        LazyValue<Weapon> currentWeapon;
        float lastTime = Mathf.Infinity;

        void Awake()
        {
            weaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetDefaultWeapon);
        }

        Weapon SetDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
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
        public void EquipWeapon(WeaponConfig _weapon)
        {
            weaponConfig = _weapon;
            currentWeapon.value = AttachWeapon(_weapon);
        }

        Weapon AttachWeapon(WeaponConfig _weapon)
        {
            Animator animator = GetComponent<Animator>();
            return _weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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

            if(currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }
            if (weaponConfig.HasProjectile())
            {
                weaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
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
            return Vector3.Distance(transform.position, target.transform.position) < weaponConfig.WeaponRange;
        }

        public void Attack(GameObject _target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = _target.GetComponent<Health>();
        }
        public bool CanAttack(GameObject _target)
        {
            if(_target == null) { return false; }
            if (!GetComponent<Move>().CanMoveTo(_target.transform.position)) return false;

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
                yield return weaponConfig.WeaponDamage;
            }
        }

        public IEnumerable<float> GetPercentModifier(Stats _stat)
        {
            if (_stat == Stats.Damage)
            {
                yield return weaponConfig.PercentBonus;
            }
        }
        public object CaptureState()
        {
            return weaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }
    }
}

