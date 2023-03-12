using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, wayPointRadius);
            }
        }
    }

}
