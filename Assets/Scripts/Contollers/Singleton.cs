using UnityEngine;

namespace Controllers {
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        private static T instance;
        public static T _instance { get { return instance; } }

        protected virtual void Awake() {
            if (instance != null && this.gameObject != null) {
                Destroy(this.gameObject);
            } else {
                instance = (T)this;
            }
            DontDestroyOnLoad(transform.root.gameObject);
        }

        // protected virtual void Start() {
            
        // }
    }
}
