using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator LoadLastScene(string _saveFile)
        {
            Dictionary<string, object> state = LoadFile(_saveFile);
            if (state.ContainsKey("lastSceneIndex"))
            {
                int buildIndex = (int)state["lastSceneIndex"];
                if (buildIndex == SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            RestoreState(state);
        }
        public void Save(string _saveFile)
        {
            Dictionary<string, object> state = LoadFile(_saveFile);
            CaptureState(state);
            SaveFile(_saveFile, state);
        }

        public void Load(string _saveFile)
        {
            RestoreState(LoadFile(_saveFile));
        }
        private void SaveFile(string _saveFile, object _state)
        {
            string path = GetPathFromSaveFile(_saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, _state);
            }
        }

        private Dictionary<string, object> LoadFile(string _saveFile)
        {
            string path = GetPathFromSaveFile(_saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Dictionary<string, object>)bf.Deserialize(stream);
            }
        }

        void CaptureState(Dictionary<string, object> _state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                _state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            _state["lastSceneIndex"] = SceneManager.GetActiveScene().buildIndex;
        }
        void RestoreState(Dictionary<string, object> _state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (_state.ContainsKey(id))
                {
                    saveable.RestoreState(_state[id]);
                }
            }
        }

        string GetPathFromSaveFile(string _saveFile)
        {
            return Path.Combine(Application.persistentDataPath, _saveFile + ".sav");
        }
        //byte[] SerializeVector(Vector3 _vector)
        //{
        //    // 4πŸ¿Ã∆Æ
        //    byte[] vectorBytes = new byte[3 * 4];
        //    BitConverter.GetBytes(_vector.x).CopyTo(vectorBytes, 0);
        //    BitConverter.GetBytes(_vector.y).CopyTo(vectorBytes, 4);
        //    BitConverter.GetBytes(_vector.z).CopyTo(vectorBytes, 8);
        //    return vectorBytes;
        //}
        //Vector3 DeserializeVector(byte[] _buffer)
        //{
        //    Vector3 vector = new Vector3();
        //    vector.x = BitConverter.ToSingle(_buffer, 0);
        //    vector.y = BitConverter.ToSingle(_buffer, 4);
        //    vector.z = BitConverter.ToSingle(_buffer, 8);
        //    return vector;
        //}
    }
}