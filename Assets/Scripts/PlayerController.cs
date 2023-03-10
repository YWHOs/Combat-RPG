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
            if (InteractCombat()) return;
            if (InteractMove()) return;
        }

        bool InteractCombat()
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
                return true;
            }
            return false;
        }

        bool InteractMove()
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(GetRay(), out hit);

            if (isHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Move>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
