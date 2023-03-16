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

        [SerializeField] float weaponRange = 2f;
        public float WeaponRange => weaponRange;
        [SerializeField] float weaponDamage = 15f;
        public float WeaponDamage => weaponDamage;

        [SerializeField] bool isRightHand = true;

        public void Spawn(Transform _rightHand, Transform _leftHand, Animator _anim)
        {
            if(equipPrefab != null)
            {
                Transform hand;
                if (isRightHand) hand = _rightHand;
                else hand = _leftHand;
                Instantiate(equipPrefab, hand);
            }

            if(animatorOverride != null)
                _anim.runtimeAnimatorController = animatorOverride;
        }
    }
}

