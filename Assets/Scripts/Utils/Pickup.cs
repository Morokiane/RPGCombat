using UnityEngine;

namespace Utils {
    public class Pickup : MonoBehaviour {
        [SerializeField] private float pickupDistance;
        [SerializeField] private float moveSpeed = 0.4f;
        [SerializeField] private float accelRate = 0.2f;

        private Vector2 moveDir;
        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
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
    }
}
