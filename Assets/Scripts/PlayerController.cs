using RPG.Combat;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            InteractCombat();
            InteractMove();
        }

        void InteractCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }
        void InteractMove()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(GetRay(), out hit);

            if (isHit)
            {
                GetComponent<Move>().MoveTo(hit.point);
            }
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
