using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObject : MonoBehaviour
    {
        [SerializeField] GameObject go;

        static bool isSpawn;

        private void Awake()
        {
            if (isSpawn) return;

            SpawnObjects();

            isSpawn = true;
        }

        void SpawnObjects()
        {
            GameObject persistent = Instantiate(go);
            DontDestroyOnLoad(persistent);
        }
    }

}
