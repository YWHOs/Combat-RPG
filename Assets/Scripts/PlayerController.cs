using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        [SerializeField] float maxNavMeshDistance = 1f;

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
            RaycastHit[] hits = RaycastAllSorted();
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
        RaycastHit[] RaycastAllSorted()
        {
            // 객체가 겹쳤을 때, 거리 계산으로 가까운거 가져오기
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
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
            Vector3 target;
            bool isHit = RaycastNavMesh(out target);
            if (isHit)
            {
                if (!GetComponent<Move>().CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Move>().StartMove(target, 1f);
                }
                SetCursor(CursorType.Move);
                return true;
            }
            return false;
        }
        bool RaycastNavMesh(out Vector3 _target)
        {
            _target = new Vector3();
            RaycastHit hit;
            bool isHit = Physics.Raycast(GetRay(), out hit);
            if (!isHit) return false;

            NavMeshHit navHit;
            bool isCastToNavMesh = NavMesh.SamplePosition(hit.point, out navHit, maxNavMeshDistance, NavMesh.AllAreas);
            if (!isCastToNavMesh) return false;

            _target = navHit.position;

            // 먼 길이는 이동 불가능하게 Path 계산
            //NavMeshPath path = new NavMeshPath();
            //bool hasPath = NavMesh.CalculatePath(transform.position, _target, NavMesh.AllAreas, path);
            //if (!hasPath) return false;
            //if (path.status != NavMeshPathStatus.PathComplete) return false;
            //if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
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
