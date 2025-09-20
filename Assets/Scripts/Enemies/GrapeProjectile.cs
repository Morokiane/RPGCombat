using System.Collections;
using UnityEngine;

namespace Enemies {
    public class GrapeProjectile : MonoBehaviour {
        [SerializeField] private float duration = 1f;
        [SerializeField] private float heightY = 3f;
        [SerializeField] private AnimationCurve animCurve;
        [SerializeField] private GameObject shadow;
        [SerializeField] private GameObject splatter;
        
        private void Start() {
            GameObject shadowGO = Instantiate(shadow, (Vector2)transform.position + new Vector2(0, -0.3f), Quaternion.identity);
            Vector2 playerPos = Player.Player._instance.transform.position;
            Vector2 shadowPos = shadowGO.transform.position;
            
            StartCoroutine(ProjectileCurve(transform.position, playerPos));
            StartCoroutine(MoveShadow(shadowGO, shadowPos, playerPos));
        }

        private IEnumerator ProjectileCurve(Vector2 _startPos, Vector2 _endPos) {
            float timePassed = 0f;

            while (timePassed < duration) {
                timePassed += Time.deltaTime;
                float linearT = timePassed / duration;
                float heightT = animCurve.Evaluate(linearT);
                float height = Mathf.Lerp(0, heightY, heightT);
                
                transform.position = Vector2.Lerp(_startPos, _endPos, linearT) + new Vector2(0, height) * Vector2.up;
                yield return null;
            }
            
            Instantiate(splatter, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private IEnumerator MoveShadow(GameObject _shadow, Vector2 _startPos, Vector2 _endPos) {
            float timePassed = 0f;

            while (timePassed < duration) {
                timePassed += Time.deltaTime;
                float linearT = timePassed / duration;
                _shadow.transform.position = Vector2.Lerp(_startPos, _endPos, linearT);
                yield return null;
            }
            Destroy(_shadow);
        }
    }
}