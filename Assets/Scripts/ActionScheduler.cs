using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour _action)
        {
            if (currentAction == _action) return;
            currentAction = _action;
        }
    }
}

