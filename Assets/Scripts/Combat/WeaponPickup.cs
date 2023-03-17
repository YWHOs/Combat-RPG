using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSecondsCo(respawnTime));
            }
        }

        IEnumerator HideForSecondsCo(float _second)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(_second);
            ShowPickup(true);
        }
        void ShowPickup(bool _canShow)
        {
            GetComponent<Collider>().enabled = _canShow;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(_canShow);
            }
        }
    }

}
