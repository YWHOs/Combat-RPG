using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    void UpdateAnimation()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 local = transform.InverseTransformDirection(velocity);
        float speed = local.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }
}
