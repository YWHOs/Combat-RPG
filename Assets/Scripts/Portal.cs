using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

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
            yield return fader.FadeInCo(fadeTime);

            // Save Current Level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            // Load Current Level
            wrapper.Load();

            // 씬 이동시 spawn위치로 플레이어 이동
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(0.5f);
            yield return fader.FadeOutCo(fadeTime);

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
