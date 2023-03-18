using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject equipPrefab;
        [SerializeField] Projectile projectile;

        [SerializeField] float weaponRange = 2f;
        public float WeaponRange => weaponRange;
        [SerializeField] float weaponDamage = 15f;
        public float WeaponDamage => weaponDamage;

        [SerializeField] bool isRightHand = true;

        const string weaponName = "weapon";

        public void Spawn(Transform _rightHand, Transform _leftHand, Animator _anim)
        {
            DestroyOldWeapon(_rightHand, _leftHand);

            if(equipPrefab != null)
            {
                Transform hand = GetHandTransform(_rightHand, _leftHand);
                GameObject weapon = Instantiate(equipPrefab, hand);
                weapon.name = weaponName;
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
        public void LaunchProjectile(Transform _rightHand, Transform _leftHand, Health _target, GameObject _instigator)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(_rightHand, _leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(_target, _instigator, weaponDamage);
        }
    }
}

