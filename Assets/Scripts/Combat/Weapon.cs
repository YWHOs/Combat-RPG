using RPG.Core;
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

        public void Spawn(Transform _rightHand, Transform _leftHand, Animator _anim)
        {
            if(equipPrefab != null)
            {
                Transform hand = GetHandTransform(_rightHand, _leftHand);
                Instantiate(equipPrefab, hand);
            }

            if (animatorOverride != null)
                _anim.runtimeAnimatorController = animatorOverride;
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
        public void LaunchProjectile(Transform _rightHand, Transform _leftHand, Health _target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(_rightHand, _leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(_target, weaponDamage);
        }
    }
}

