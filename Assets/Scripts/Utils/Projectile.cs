using UnityEngine;
using Inventory;
using Player;

namespace Utils {
    public class Projectile : MonoBehaviour {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private GameObject particleOnHitPrefab;
        [SerializeField] private bool isEnemyProjectile;
        [SerializeField] private float projectileRange = 10f;

        private Vector2 startPos;

        private void Start() {
            startPos = transform.position;
        }
        
        private void FixedUpdate() {
            MoveProjectile();
            DetectFireDistance();
        }
        
        private void DetectFireDistance() {
            if (Vector2.Distance(transform.position, startPos) > projectileRange) {
                Destroy(gameObject);
            }
        }

        public void UpdateProjectileRange(float projectileRange) {
            this.projectileRange = projectileRange;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Enemies.EnemyHealth enemyHealth = other.gameObject.GetComponent<Enemies.EnemyHealth>();
            Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            
            // TODO This needs to be refactored
            if (!other.isTrigger && (enemyHealth || indestructible || player)) {
                if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile)) {
                    player?.TakeDamage(1, transform);
                    Instantiate(particleOnHitPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                } else if (!other.isTrigger && indestructible) {
                    Instantiate(particleOnHitPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }

        private void MoveProjectile() {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
        }
    }
}
