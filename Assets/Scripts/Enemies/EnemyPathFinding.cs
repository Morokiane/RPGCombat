using UnityEngine;
using Utils;

namespace Enemies {
	public class EnemyPathFinding : MonoBehaviour {
		[SerializeField] private float moveSpeed = 2;

		private Rigidbody2D rb;
		private Vector2 moveDir;
		private Knockback knockback;

		private void Awake() {
			rb = GetComponent<Rigidbody2D>();
			knockback = GetComponent<Knockback>();
		}

		private void FixedUpdate() {
			if (knockback.gettingKnockedUp) return;
		    rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
		}

		public void MoveTo(Vector2 targetPosition) {
			moveDir = targetPosition;
		}
	}
}
