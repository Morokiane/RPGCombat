using UnityEngine;
using System.Collections;
using Utils;
using Enemies;

namespace Player {
    public class PlayerHealth : MonoBehaviour {
        [SerializeField] private uint maxHealth = 3;
        [SerializeField] private float knockbackAmount = 10f;
        [SerializeField] private float recoverTime = 0.5f;

        private uint currentHealth;
        private bool canTakeDamage = true;

        private Knockback knockback;
        private Flash flash;

        private void Awake() {
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
        }

        private void Start() {
            currentHealth = maxHealth;
        }

        private void OnCollisionStay2D(Collision2D other) {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            
            if (enemy) {
                TakeDamage(1, other.transform);
            }
            // if (other.CompareTag("Enemy")) {
            //     TakeDamage(1, other.transform);
            // }
        }

        public void TakeDamage(uint damageAmount, Transform hitTransform) {
            if (!canTakeDamage) return;

            knockback.GetKnockback(hitTransform.gameObject.transform, knockbackAmount);
            StartCoroutine(flash.FlashRoutine());
            currentHealth -= damageAmount;
            canTakeDamage = false;
            StartCoroutine(ResetDamage());
        }

        private IEnumerator ResetDamage() {
            yield return new WaitForSeconds(recoverTime);
            canTakeDamage = true;
        }
    }
}