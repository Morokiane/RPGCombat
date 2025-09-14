using System.Collections;
using UnityEngine;

namespace Utils {
    public class Knockback : MonoBehaviour {

        public bool gettingKnockedUp { get; private set; }
        [SerializeField] private float knockBackTime = 0.2f;

        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        public void GetKnockback(Transform damageSource, float knockbackAmount) {
            gettingKnockedUp = true;
            Vector2 force = (transform.position - damageSource.position).normalized * knockbackAmount;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(KnockbackTime());
        }

        private IEnumerator KnockbackTime() {
            yield return new WaitForSeconds(knockBackTime);
            rb.linearVelocity = Vector2.zero;
            gettingKnockedUp = false;
        }
    }
}
