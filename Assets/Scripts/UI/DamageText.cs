using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Text damageText; 
        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float _value)
        {
            damageText.text = string.Format("{0:0}", _value);
        }
    }

}