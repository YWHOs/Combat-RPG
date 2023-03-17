using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] bool isHoming;
    float damage;

    Health target;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }
    void Update()
    {
        if (target == null) return;

        if(isHoming && !target.IsDead())
            transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }
    Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead()) return;
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
