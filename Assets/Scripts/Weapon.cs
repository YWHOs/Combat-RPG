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

        public void Spawn(Transform _hand, Animator _anim)
        {
            if(equipPrefab != null)
                Instantiate(equipPrefab, _hand);
            if(animatorOverride != null)
                _anim.runtimeAnimatorController = animatorOverride;
        }
    }
}

