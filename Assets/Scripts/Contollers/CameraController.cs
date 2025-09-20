using Unity.Cinemachine;

namespace Controllers {
    public class CameraController : Singleton<CameraController> {
        private CinemachineCamera cinemachineCamera;

        private void Start() {
            SetPlayerCameraFollow();
        }

        public void SetPlayerCameraFollow() {
            cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
            cinemachineCamera.Follow = Player.Player._instance.transform;
        }
    }
}
