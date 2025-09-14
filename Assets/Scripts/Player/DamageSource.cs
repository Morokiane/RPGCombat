using UnityEngine;
using Enemies;

namespace Player {
    public class DamageSource : MonoBehaviour {
        [SerializeField] private uint damageAmount;
        
        private void OnTriggerEnter2D(Collider2D other) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
        }
    }
}
