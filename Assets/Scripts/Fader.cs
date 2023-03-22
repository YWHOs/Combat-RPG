using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup cvGroup;
        Coroutine currentActive = null;

        void Awake()
        {
            cvGroup = GetComponent<CanvasGroup>();
        }
        public void FadeInImmediate()
        {
            cvGroup.alpha = 1;
        }
        public IEnumerator FadeInCo(float _time)
        {
            return FadeCo(1, _time);
        }
        public IEnumerator FadeOutCo(float _time)
        {
            return FadeCo(0, _time);
        }

        public IEnumerator FadeCo(float _target, float _time)
        {
            if (currentActive != null)
            {
                StopCoroutine(currentActive);
            }
            currentActive = StartCoroutine(FadeRoutine(_target, _time));
            yield return currentActive;
        }
        IEnumerator FadeRoutine(float _target, float _time)
        {
            while (!Mathf.Approximately(cvGroup.alpha, _target))
            {
                cvGroup.alpha = Mathf.MoveTowards(cvGroup.alpha, _target, Time.deltaTime / _time);
                yield return null;
            }
        }
    }

}
