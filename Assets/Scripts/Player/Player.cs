using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        private static readonly int moveX = Animator.StringToHash("moveX");
        private static readonly int moveY = Animator.StringToHash("moveY");

        [SerializeField] private float moveSpeed = 4f;

        private Vector2 movement;
        private Rigidbody2D rb;
        private Animator anim;
        private PlayerControls playerControls;
        private SpriteRenderer sprite;
        private Camera mainCamera;

        private void Awake() {
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            playerControls = new PlayerControls();
        }

        private void Update() {
            PlayerInput();
        }

        private void FixedUpdate() {
            Move();
            AdjustPlayerFacing();
        }

        private void Move() {
            rb.MovePosition(rb.position + movement *(moveSpeed * Time.deltaTime));
        }

        private void AdjustPlayerFacing() {
            var mousePos = Input.mousePosition;

            if (mainCamera != null) {
                Vector3 playerScreenPoint = mainCamera.WorldToScreenPoint(transform.position);

                if (mousePos.x < playerScreenPoint.x) {
                    sprite.flipX = true;
                } else {
                    sprite.flipX = false;
                }
            }
        }

        private void PlayerInput() {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();
            anim.SetFloat(moveX, movement.x);
            anim.SetFloat(moveY, movement.y);
        }

        private void OnEnable() {
            playerControls.Enable();
        }
    }
}
