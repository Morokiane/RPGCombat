using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils {
    public class MouseFollow : MonoBehaviour {
        private void Update() {
            FaceMouse();
        }

        private void FaceMouse() {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 direction = transform.position - mousePos;
            transform.right = -direction;
        }
    }
}
