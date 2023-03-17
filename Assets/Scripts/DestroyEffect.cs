using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyEffect : MonoBehaviour
    {

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

}