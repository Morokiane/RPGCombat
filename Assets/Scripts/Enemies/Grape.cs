using UnityEngine;

namespace Enemies {
    public class Grape : MonoBehaviour, IEnemy {
        readonly int attack = Animator.StringToHash("Attack");

        [SerializeField] private GameObject grapeProjectile;

        private Animator anim;
        private SpriteRenderer sprite;

        private void Awake() {
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        public void Attack() {
            anim.SetTrigger(attack);

            if (transform.position.x - Player.Player._instance.transform.position.x < 0) {
                sprite.flipX = false;
            } else {
                sprite.flipX = true;
            }
        }

        public void SpawnProjectile() {
            Instantiate(grapeProjectile, transform.position, Quaternion.identity);
        }
    }
}