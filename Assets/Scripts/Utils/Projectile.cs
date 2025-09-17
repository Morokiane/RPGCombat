using UnityEngine;
using Inventory;

namespace Utils {
    public class Projectile : MonoBehaviour {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private GameObject particleOnHitPrefab;

        private WeaponInfo weaponInfo;
        private Vector2 startPos;

        private void Start() {
            startPos = transform.position;
        }
        
        private void FixedUpdate() {
            MoveProjectile();
            DetectFireDistance();
        }
        
        private void DetectFireDistance() {
            if (Vector2.Distance(transform.position, startPos) > weaponInfo.weaponRange) {
                Destroy(gameObject);
            }
        }

        public void UpdateWeaponInfo(WeaponInfo _weaponInfo) {
            weaponInfo = _weaponInfo;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Enemies.EnemyHealth enemyHealth = other.gameObject.GetComponent<Enemies.EnemyHealth>();
            Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

            if (!other.isTrigger && (enemyHealth || indestructible)) {
                enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
                Instantiate(particleOnHitPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void MoveProjectile() {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
        }
    }
}
