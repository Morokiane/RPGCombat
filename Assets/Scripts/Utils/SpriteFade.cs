using UnityEngine;
using System.Collections;

namespace Utils {
    public class SpriteFade : MonoBehaviour {
        [SerializeField] private float fadeTime = 0.4f;

        private SpriteRenderer sprite;

        private void Awake() {
            sprite = GetComponent<SpriteRenderer>();
        }

        public IEnumerator SlowFade() {
            float elapsedTime = 0f;
            float startValue = sprite.color.a;
            
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / fadeTime);
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
