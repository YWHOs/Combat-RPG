using RPG.Control;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRayCastable
    {
        [SerializeField] WeaponConfig weapon;
        [SerializeField] float healthRecover;
        [SerializeField] float respawnTime = 5f;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other.gameObject);
            }
        }

        void Pickup(GameObject _subject)
        {
            if(weapon != null)
            {
                _subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            if(healthRecover > 0)
            {
                _subject.GetComponent<Health>().Heal(healthRecover);
            }
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
                Pickup(_callController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

}
