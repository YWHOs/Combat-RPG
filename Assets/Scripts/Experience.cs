using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float expPoint;

        public void GetExp(float _exp)
        {
            expPoint += _exp;
        }
    }

}
