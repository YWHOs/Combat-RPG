using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {

        void Update()
        {
            // UI �� ����
            transform.forward = Camera.main.transform.forward;
        }
    }

}
