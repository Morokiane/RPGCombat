using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Utils {
    public class Fade : Controllers.Singleton<Fade> {
        
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeSpeed = 1f;

        private IEnumerator fadeRoutine;

        public void FadeToBlack() {
            if (fadeRoutine != null) {
                StopCoroutine(fadeRoutine);
            }

            fadeRoutine = Fader(1);
            StartCoroutine(fadeRoutine);
        }

        public void FadeToClear() {
            if (fadeRoutine != null) {
                StopCoroutine(fadeRoutine);
            }

            fadeRoutine = Fader(0);
            StartCoroutine(fadeRoutine);
        }

        private IEnumerator Fader(float targetAlpha) {
            while (!Mathf.Approximately(fadeImage.color.a, targetAlpha)) {
                float alpha = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                yield return null;
            }
        }
    }
}
