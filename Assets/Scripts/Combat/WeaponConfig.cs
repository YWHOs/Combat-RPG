using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] Weapon equipPrefab;
        [SerializeField] Projectile projectile;

        [SerializeField] float weaponRange = 2f;
        public float WeaponRange => weaponRange;
        [SerializeField] float weaponDamage = 15f;
        public float WeaponDamage => weaponDamage;
        [SerializeField] float percentBonus;
        public float PercentBonus => percentBonus;

        [SerializeField] bool isRightHand = true;

        const string weaponName = "weapon";

        public Weapon Spawn(Transform _rightHand, Transform _leftHand, Animator _anim)
        {
            DestroyOldWeapon(_rightHand, _leftHand);

            Weapon weapon = null;
            if (equipPrefab != null)
            {
                Transform hand = GetHandTransform(_rightHand, _leftHand);
                weapon = Instantiate(equipPrefab, hand);
                weapon.gameObject.name = weaponName;
            }

            var overrideController = _anim.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                _anim.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                _anim.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            return weapon;
        }

        void DestroyOldWeapon(Transform _rightHand, Transform _leftHand)
        {
            Transform oldWeapon = _rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = _leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "Destroy";
            Destroy(oldWeapon.gameObject);
        }

        Transform GetHandTransform(Transform _rightHand, Transform _leftHand)
        {
            Transform hand;
            if (isRightHand) hand = _rightHand;
            else hand = _leftHand;
            return hand;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        public void LaunchProjectile(Transform _rightHand, Transform _leftHand, Health _target, GameObject _instigator, float _calculateDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(_rightHand, _leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(_target, _instigator, _calculateDamage);
        }
    }
}

