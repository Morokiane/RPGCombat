using Unity.Cinemachine;

namespace Controllers {
    public class CameraController : Singleton<CameraController> {
        private CinemachineCamera cinemachineCamera;

        public void SetPlayerCameraFollow() {
            cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
            cinemachineCamera.Follow = Player.Player._instance.transform;
        }
    }
}
