using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup cvGroup;

        void Start()
        {
            cvGroup = GetComponent<CanvasGroup>();
        }
        public void FadeInImmediate()
        {
            cvGroup.alpha = 1;
        }
        public IEnumerator FadeInCo(float _time)
        {
            while(cvGroup.alpha < 1)
            {
                cvGroup.alpha += Time.deltaTime / _time;
                yield return null;
            }
        }
        public IEnumerator FadeOutCo(float _time)
        {
            while (cvGroup.alpha > 0)
            {
                cvGroup.alpha -= Time.deltaTime / _time;
                yield return null;
            }
        }
    }

}
