using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

    }

    void MoveToCursor()
    {
        Ray rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(rayPoint, out hit);

        if (isHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
