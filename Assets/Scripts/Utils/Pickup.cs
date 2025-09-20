using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils {
    public class Pickup : MonoBehaviour {
        [SerializeField] private float pickupDistance;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float accelRate = 0.5f;
        [SerializeField] private float heightY = 2f;
        [SerializeField] private float popDuration = 0.25f;
        [SerializeField] private AnimationCurve animCurve;
        
        private Vector2 moveDir;
        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            StartCoroutine(SpawnAnim());
        }

        private void Update() {
            Vector2 playerPos = Player.Player._instance.transform.position;

            if (Vector2.Distance(transform.position, playerPos) < pickupDistance) {
                moveDir = (playerPos - (Vector2)transform.position).normalized;
                moveSpeed += accelRate;
            } else {
                moveDir = Vector2.zero;
                moveSpeed = 0f;
            }
        }

        private void FixedUpdate() {
            rb.linearVelocity = moveDir * (moveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D _other) {
            if (_other.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }

        private IEnumerator SpawnAnim() {
            Vector2 startPoint = transform.position;
            float randomX = transform.position.x + Random.Range(-2f, 2f);
            float randomY = transform.position.y + Random.Range(-1f, 1f);
            
            Vector2 endPoint = new Vector2(randomX, randomY);
            float timePassed = 0f;

            while (timePassed < popDuration) {
                timePassed += Time.deltaTime;
                float linearT = timePassed / popDuration;
                float heightT = animCurve.Evaluate(linearT);
                float height = Mathf.Lerp(0f, heightY, heightT);
                
                transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
                yield return null;
            }
        }
    }
}
