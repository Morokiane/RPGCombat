using UnityEngine;

namespace Controllers {
    public class LevelController : Singleton<LevelController> {
        public string sceneTransitionName { get; private set; }

        public void SetTransitionName(string sceneName) {
            this.sceneTransitionName = sceneName;
        }
    }
}
