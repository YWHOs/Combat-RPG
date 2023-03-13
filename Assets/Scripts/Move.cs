using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent nav;
        Health health;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        // Update is called once per frame
        void Update()
        {
            nav.enabled = !health.IsDead();
            UpdateAnimation();
        }

        public void StartMove(Vector3 _destination, float _speed)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(_destination, _speed);
        }
        public void MoveTo(Vector3 _destination, float _speed)
        {
            nav.destination = _destination;
            nav.speed = maxSpeed * Mathf.Clamp01(_speed);
            nav.isStopped = false;
        }
        public void Cancel()
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

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }

}
