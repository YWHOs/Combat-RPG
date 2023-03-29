using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy;

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if(targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                    Destroy(gameObject);
            }
        }
    }

}