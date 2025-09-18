using UnityEngine;
using Enemies;

namespace Player {
    public class DamageSource : MonoBehaviour {
        private uint damageAmount;

        private void Start() {
            MonoBehaviour currentActiveWeapon = ActiveWeapon._instance.currentActiveWeapon;
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;            
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
        }
    }
}
