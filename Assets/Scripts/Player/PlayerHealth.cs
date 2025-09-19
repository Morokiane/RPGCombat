using UnityEngine;
using System.Collections;
using Utils;

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

        private void OnTriggerStay2D(Collider2D other) {
            if (other.CompareTag("Enemy") && canTakeDamage) {
                TakeDamage(1);
                knockback.GetKnockback(other.gameObject.transform, knockbackAmount);
                StartCoroutine(flash.FlashRoutine());
            }
        }

        private void TakeDamage(uint damageAmount) {
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