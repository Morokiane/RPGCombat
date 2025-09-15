using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

namespace Utils {
    public class TransparentDetection : MonoBehaviour {
        [Range(0, 1)]
        [SerializeField] private float transparencyAmount = 0.8f;
        [SerializeField] private float fadeTime = 0.4f;

        private SpriteRenderer sprite;
        private Tilemap tilemap;

        private void Awake() {
            sprite = GetComponent<SpriteRenderer>();
            tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                if (sprite) {
                    StartCoroutine(FadeRoutine(sprite, fadeTime, sprite.color.a, transparencyAmount));
                } else if (tilemap) {
                    StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                if (sprite) {
                    StartCoroutine(FadeRoutine(sprite, fadeTime, sprite.color.a, 1f));
                } else if (tilemap) {
                    if (!isActiveAndEnabled) return; // This stops the game from crashing when unloading the scene we are leaving
                    StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
                }
            }
        }

        private IEnumerator FadeRoutine(SpriteRenderer sprite, float fadeTime, float startValue, float targetTransparency) {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
                yield return null;
            }
        }

        private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency) {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
                yield return null;
            }
        }
    }
}
