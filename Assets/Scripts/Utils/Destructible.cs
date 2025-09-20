using UnityEngine;

namespace Utils {
    public class Destructible : MonoBehaviour {
        [SerializeField] private GameObject destroyVFX;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<Player.DamageSource>()) {
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                GetComponent<PickupSpawner>().DropItems();
                Destroy(gameObject);
            }
        }
    }
}
