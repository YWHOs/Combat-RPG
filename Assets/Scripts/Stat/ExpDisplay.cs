using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    public class ExpDisplay : MonoBehaviour
    {
        Experience exp;
        void Awake()
        {
            exp = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = exp.GetPoint().ToString();
        }
    }

}
