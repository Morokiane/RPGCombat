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

        public void UpdateProjectileRange(float _projectileRange) {
            projectileRange = _projectileRange;
        }

        public void UpdateMoveSpeed(float _moveSpeed) {
            moveSpeed = _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D _other) {
            Enemies.EnemyHealth enemyHealth = _other.gameObject.GetComponent<Enemies.EnemyHealth>();
            Indestructible indestructible = _other.gameObject.GetComponent<Indestructible>();
            PlayerHealth player = _other.gameObject.GetComponent<PlayerHealth>();
            
            // TODO This needs to be refactored
            if (!_other.isTrigger && (enemyHealth || indestructible || player)) {
                if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile)) {
                    player?.TakeDamage(1, transform);
                    Instantiate(particleOnHitPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                } else if (!_other.isTrigger && indestructible) {
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
