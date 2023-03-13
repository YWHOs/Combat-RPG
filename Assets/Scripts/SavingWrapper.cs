using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string saveFile = "save";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        void Load()
        {
            // Call Saving System
            GetComponent<SavingSystem>().Load(saveFile);
        }

        void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }
    }
}