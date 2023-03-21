using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] GameObject destroy;

    public void DestroyTarget()
    {
        Destroy(destroy);
    }
}
