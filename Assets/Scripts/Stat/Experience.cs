using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expPoint;

        //public delegate void ExperienceGainDelegate();
        public event Action onExperienceGain;



        public void GetExp(float _exp)
        {
            expPoint += _exp;

            onExperienceGain();
        }
        public float GetPoint()
        {
            return expPoint;
        }
        public object CaptureState()
        {
            return expPoint;
        }
        public void RestoreState(object state)
        {
            expPoint = (float)state;
        }
    }

}
