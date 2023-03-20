using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings; 

        void Awake()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithComponent()) return;
            if (InteractMove()) return;
            SetCursor(CursorType.None);
        }

        bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach (RaycastHit hit in hits)
            {
                IRayCastable[] raycastables = hit.transform.GetComponents<IRayCastable>();
                foreach(IRayCastable ray in raycastables)
                {
                    if (ray.HandleRaycast(this))
                    {
                        SetCursor(ray.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
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
                    GetComponent<Move>().StartMove(hit.point, 1f);
                }
                SetCursor(CursorType.Move);
                return true;
            }
            return false;
        }
        void SetCursor(CursorType _type)
        {
            CursorMapping mapping = GetCursorMapping(_type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }
        CursorMapping GetCursorMapping(CursorType _type)
        {
            foreach(CursorMapping map in cursorMappings)
            {
                if(map.type == _type)
                {
                    return map;
                }
            }
            return cursorMappings[0];
        }
        static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
