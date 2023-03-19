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
        void Awake()
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

        [System.Serializable]
        struct MoveSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoveSaveData data = new MoveSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data["position"] = new SerializableVector3(transform.position);
            //data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoveSaveData data = (MoveSaveData)state;
            //Dictionary<string, object> data = (Dictionary<string, object>)state;
            nav.enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            //transform.position = ((SerializableVector3)data["position"]).ToVector();
            //transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            nav.enabled = true;
            GetComponent<ActionScheduler>().CancelAction();
        }
    }

}
