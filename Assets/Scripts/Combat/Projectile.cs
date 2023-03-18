using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming;
        [SerializeField] GameObject hitEffect;
        [SerializeField] float lifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit;
        [SerializeField] float lifeAfterImpact = 2f;
        float damage;
        GameObject instigator;

        Health target;

        void Start()
        {
            transform.LookAt(GetAimLocation());
        }
        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
                transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        public void SetTarget(Health _target, GameObject _instigator, float _damage)
        {
            target = _target;
            damage = _damage;
            instigator = _instigator;

            Destroy(gameObject, lifeTime);
        }
        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(instigator, damage);

            speed = 0;

            if (hitEffect != null)
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            foreach (GameObject destroy in destroyOnHit)
            {
                Destroy(destroy);
            }
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
