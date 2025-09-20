using UnityEngine;

namespace Enemies {
    public class Shooter : MonoBehaviour, IEnemy {
        [SerializeField] private GameObject bullet;

        public void Attack() {
            Vector2 targetDir = Player.Player._instance.transform.position - transform.position;

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDir;
        }
    }
}