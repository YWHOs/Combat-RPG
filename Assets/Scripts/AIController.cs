using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDt = 5f;
        [SerializeField] float timeSuspicion = 4f;

        Vector3 originPos;
        float timeSawPlayer = Mathf.Infinity;

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
                timeSawPlayer = 0f;
                AttackBehaviour();
            }
            else if (timeSawPlayer < timeSuspicion)
            {
                SuspicionBehaviour();
            }
            else
            {
                move.StartMove(originPos);
            }

            timeSawPlayer += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        private void AttackBehaviour()
        {
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
