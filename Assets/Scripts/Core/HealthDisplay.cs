using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = string.Format("{0:0}/{1:0}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}

