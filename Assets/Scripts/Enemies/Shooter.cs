using UnityEngine;
using System.Collections;

namespace Enemies {
    public class Shooter : MonoBehaviour, IEnemy {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletMoveSpeed;
        [SerializeField] private uint burstCount;
        [SerializeField] private float timeBetweenBursts;
        [SerializeField] private float cooldown = 1f;

        private bool isShooting;

        public void Attack() {
            if (!isShooting) {
                StartCoroutine(Shooting());
            }
        }

        private IEnumerator Shooting() {
            isShooting = true;

            for (int i = 0; i < burstCount; i++) {
                Vector2 targetDir = Player.Player._instance.transform.position - transform.position;

                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.transform.right = targetDir;

                if (newBullet.TryGetComponent(out Utils.Projectile projectile)) {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                yield return new WaitForSeconds(timeBetweenBursts);
            }

    
            yield return new WaitForSeconds(cooldown);
            isShooting = false;
        }
    }
}