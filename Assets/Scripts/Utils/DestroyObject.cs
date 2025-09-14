using UnityEngine;

namespace Utils {
    public class DestroyObject : MonoBehaviour {
        [SerializeField] private float destroyTime;

        private ParticleSystem ps;

        private void Awake() {
            ps = GetComponent<ParticleSystem>();
        }

        private void Update() {
            if (ps && !ps.IsAlive()) {
                Destroy();
            } 
        }

        public void Destroy() {
            Destroy(gameObject, destroyTime);
        } 
    }
}
