using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction _action)
        {
            if (currentAction == _action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = _action;
        }
    }
}

