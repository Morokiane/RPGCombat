using UnityEngine;

namespace Utils {
    public class AreaEnterance : MonoBehaviour {
        [SerializeField] private string transitionName;

        private void Start() {
            if (transitionName == Controllers.LevelController._instance.sceneTransitionName) {
                Player.Player._instance.transform.position = transform.position;
                Controllers.CameraController._instance.SetPlayerCameraFollow();
                Utils.Fade._instance.FadeToClear();
            }
        }
    }
}
