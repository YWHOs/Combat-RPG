using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRayCastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController _callController)
        {
            if (!enabled) return false;
            if (!_callController.GetComponent<Fighter>().CanAttack(gameObject)) return false;

            if (Input.GetMouseButton(0))
            {
                _callController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}

