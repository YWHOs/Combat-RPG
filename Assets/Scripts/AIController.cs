using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDt = 5f;
        [SerializeField] float timeSuspicion = 4f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypoint = 1f;
        [SerializeField] float waypointDwellTime = 3f;

        Vector3 originPos;
        float timeSawPlayer = Mathf.Infinity;
        float timeArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypoint;

        Fighter fighter;
        Health health;
        Move move;
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            move = GetComponent<Move>();
            player = GameObject.FindWithTag("Player");

            originPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            if (IsAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSawPlayer < timeSuspicion)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTime();
        }

        private void UpdateTime()
        {
            timeSawPlayer += Time.deltaTime;
            timeArrivedAtWaypoint += Time.deltaTime;
        }

        void PatrolBehaviour()
        {
            Vector3 nextPos = originPos;
            if(patrolPath != null)
            {
                if (IsWaypoint())
                {
                    timeArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPos = GetWaypoint();
            }
            if (timeArrivedAtWaypoint > waypointDwellTime)
            {
                move.StartMove(nextPos);
            }
        }

        Vector3 GetWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypoint);
        }

        void CycleWaypoint()
        {
            currentWaypoint = patrolPath.GetNextIndex(currentWaypoint);
        }

        bool IsWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetWaypoint());
            return distanceToWaypoint < waypoint;
        }

        void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        void AttackBehaviour()
        {
            timeSawPlayer = 0f;
            fighter.Attack(player);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDt);
        }

        bool IsAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDt;
        }
    }

}
