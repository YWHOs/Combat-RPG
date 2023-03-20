using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRayCastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        void Pickup(Fighter _fighter)
        {
            _fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSecondsCo(respawnTime));
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

        public bool HandleRaycast(PlayerController _callController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(_callController.GetComponent<Fighter>());
            }
            return true;
        }
    }

}
