using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTrigger;
        void OnTriggerEnter(Collider other)
        {
            if(!isTrigger && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                isTrigger = true;
            }

        }
    }

}
