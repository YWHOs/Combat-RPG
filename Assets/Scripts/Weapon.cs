using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject weaponPrefab;

        public void Spawn(Transform _hand, Animator _anim)
        {
            Instantiate(weaponPrefab, _hand);
            _anim.runtimeAnimatorController = animatorOverride;
        }
    }
}

