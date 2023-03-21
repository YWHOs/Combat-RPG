using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawn : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;



        public void Spawn(float _damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }

}