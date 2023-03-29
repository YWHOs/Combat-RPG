using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicCtonrolRemover : MonoBehaviour
    {
        GameObject player;
        // Observer
        void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }
        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
        void EnableControl(PlayableDirector _pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
        void DisableControl(PlayableDirector _pd)
        {
            player.GetComponent<ActionScheduler>().CancelAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
    }

}
