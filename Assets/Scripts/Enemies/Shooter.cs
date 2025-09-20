using UnityEngine;
using System.Collections;

namespace Enemies {
    public class Shooter : MonoBehaviour, IEnemy {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletMoveSpeed;
        [SerializeField] private uint burstCount;
        [SerializeField] private float timeBetweenBursts;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private uint projectilesPerBurst;
        [SerializeField][Range(0, 359)] private float angleSpread;
        [SerializeField] private float startingDistance = 0.1f;
        [SerializeField] private bool stagger;
        [SerializeField] private bool oscillate;

        private bool isShooting;

        // Make sure settings don't break by having a minimal set amount in the Inspector
        private void OnValidate() {
            if (oscillate) stagger = true;
            if (!oscillate) stagger = false;
            if (projectilesPerBurst < 1) projectilesPerBurst = 1;
            if (burstCount < 1) burstCount = 1;
            if (timeBetweenBursts < 0.1f) timeBetweenBursts = 0.1f;
            if (cooldown < 0.1f) cooldown = 0.1f;
            if (startingDistance < 0.1f) startingDistance = 0.1f;
            if (angleSpread == 0) projectilesPerBurst = 1;
            if (bulletMoveSpeed <= 0) bulletMoveSpeed = 0.1f;
        }

        public void Attack() {
            if (!isShooting) {
                StartCoroutine(Shooting());
            }
        }

        private IEnumerator Shooting() {
            isShooting = true;

            float startAngle, currentAngle, angleStep, endAngle;
            float timeBetweenProjectiles = 0f;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

            if (stagger) timeBetweenBursts = timeBetweenBursts / projectilesPerBurst;

            for (int i = 0; i < burstCount; i++) {
                if (!oscillate) {
                    TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
                }

                if (oscillate && i % 2 != 1) {
                    TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
                } else if (oscillate) {
                    currentAngle = endAngle;
                    endAngle = startAngle;
                    startAngle = currentAngle;
                    angleStep *= -1;
                }

                for (int j = 0; j < projectilesPerBurst; j++) {
                    Vector2 pos = FindBulletSpawnPos(currentAngle);

                    GameObject newBullet = Instantiate(bullet, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;

                    if (newBullet.TryGetComponent(out Utils.Projectile projectile)) {
                        projectile.UpdateMoveSpeed(bulletMoveSpeed);
                    }

                    currentAngle += angleStep;

                    if (stagger) yield return new WaitForSeconds(timeBetweenProjectiles);
                }

                currentAngle = startAngle;

                if (!stagger) yield return new WaitForSeconds(timeBetweenBursts);
            }

            yield return new WaitForSeconds(cooldown);
            isShooting = false;
        }

        private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle) {
            Vector2 targetDir = Player.Player._instance.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            startAngle = targetAngle;
            endAngle = targetAngle;
            currentAngle = targetAngle;
            float halfAngleSpread = 0f;
            angleStep = 0;

            if (angleSpread != 0) {
                angleStep = angleSpread / (projectilesPerBurst - 1);
                halfAngleSpread = angleSpread / 2f;
                startAngle = targetAngle - halfAngleSpread;
                endAngle = targetAngle + halfAngleSpread;
                currentAngle = startAngle;
            }
        }

        private Vector2 FindBulletSpawnPos(float _currentAngle) {
            float x = transform.position.x + startingDistance * Mathf.Cos(_currentAngle * Mathf.Deg2Rad);
            float y = transform.position.y + startingDistance * Mathf.Sin(_currentAngle * Mathf.Deg2Rad);
            
            Vector2 pos = new Vector2(x, y);

            return pos;
        }
    }
}