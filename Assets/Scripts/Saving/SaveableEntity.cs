using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntity> globalLook = new Dictionary<string, SaveableEntity>();
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }
        public void RestoreState(object _state)
        {
            Dictionary<string, object> state = (Dictionary<string, object>)_state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (state.ContainsKey(typeString))
                {
                    saveable.RestoreState(state[typeString]);
                }
            }
        }
        bool Isunique(string _stringValue)
        {
            if (!globalLook.ContainsKey(_stringValue)) return true;
            if (globalLook[_stringValue] == this) return true;

            if (globalLook[_stringValue] == null)
            {
                globalLook.Remove(_stringValue);
                return true;
            }
            if (globalLook[_stringValue].GetUniqueIdentifier() != _stringValue)
            {
                globalLook.Remove(_stringValue);
                return true;
            }
            return false;
        }
        // 빌드시 삭제 (에디터에서만 사용 가능)
#if UNITY_EDITOR
        void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            
            if(string.IsNullOrEmpty(property.stringValue) || !Isunique(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLook[property.stringValue] = this;
        }
#endif

    }
}