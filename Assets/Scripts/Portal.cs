using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentify
        {
            a,
            b,
            c
        }
        [SerializeField] int sceneIndex;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentify destinationIdentify;
        [SerializeField] float fadeTime = 1f;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(TransitionCo());
            }
        }

        IEnumerator TransitionCo()
        {
            if (sceneIndex < 0) yield break;


            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            // Save Current Level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            PlayerController playerController =  GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeInCo(fadeTime);

            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneIndex);
            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            // Load Current Level
            wrapper.Load();

            // 씬 이동시 spawn위치로 플레이어 이동
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            wrapper.Save();

            yield return new WaitForSeconds(0.5f);
            yield return fader.FadeOutCo(fadeTime);

            newPlayerController.enabled = true;

            Destroy(gameObject);
        }

        void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destinationIdentify != destinationIdentify) continue;
                return portal;
            }
            return null;
        }
    }

}
