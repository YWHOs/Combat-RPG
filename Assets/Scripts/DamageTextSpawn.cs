using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawn : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void Spawn(float _damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }

}