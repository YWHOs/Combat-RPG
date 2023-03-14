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

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeInImmediate();
            //yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
            yield return fader.FadeOutCo(1f);
        }
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

        public void Load()
        {
            // Call Saving System
            GetComponent<SavingSystem>().Load(saveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }
    }
}