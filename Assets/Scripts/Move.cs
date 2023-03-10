using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Move : MonoBehaviour
    {
        NavMeshAgent nav;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
        }
        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
        }
        public void StartMove(Vector3 _destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(_destination);
        }
        public void MoveTo(Vector3 _destination)
        {
            nav.destination = _destination;
            nav.isStopped = false;
        }
        public void Stop()
        {
            nav.isStopped = true;
        }

        void UpdateAnimation()
        {
            Vector3 velocity = nav.velocity;
            Vector3 local = transform.InverseTransformDirection(velocity);
            float speed = local.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }

}
