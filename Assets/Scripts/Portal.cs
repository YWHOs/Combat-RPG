using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneIndex;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

}
