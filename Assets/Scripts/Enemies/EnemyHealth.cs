using System.Collections;
using UnityEngine;
using Utils;

namespace Enemies {
    public class EnemyHealth : MonoBehaviour {
        [SerializeField] private uint maxHealth;
        [SerializeField] private float knockBackForce = 15f;
        [SerializeField] private GameObject deathVFX;
        
        private uint currentHealth;
        private Knockback knockback;
        private Flash flash;

        private void Awake() {
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
        }

        private void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(uint damage) {
            currentHealth -= damage;
            knockback.GetKnockback(Player.Player.instance.transform, knockBackForce);
            StartCoroutine(flash.FlashRoutine());
            
            if (currentHealth <= 0) {
                StartCoroutine(KillDelay());
            }
        }

        private IEnumerator KillDelay() {
            yield return new WaitForSeconds(flash.GetRestoreMatTime());
            Kill();
        }

        private void Kill() {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
