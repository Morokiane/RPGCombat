using UnityEngine;
using Utils;

namespace Enemies {
	public class EnemyPathFinding : MonoBehaviour {
		[SerializeField] private float moveSpeed = 2;

		private Rigidbody2D rb;
		private Vector2 moveDir;
		private Knockback knockback;
		private SpriteRenderer sprite;

		private void Awake() {
			rb = GetComponent<Rigidbody2D>();
			knockback = GetComponent<Knockback>();
			sprite = GetComponent<SpriteRenderer>();
		}

		private void FixedUpdate() {
			if (knockback.gettingKnockedUp) return;
			rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

			if (moveDir.x < 0) {
				sprite.flipX = true;
			} else {
				sprite.flipX = false;
			}
		}

		public void MoveTo(Vector2 targetPosition) {
			moveDir = targetPosition;
		}

		public void StopMoving() {
			moveDir = Vector2.zero;
		}
	}
}
