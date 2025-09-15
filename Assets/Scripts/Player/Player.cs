using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Player {
    public class Player : Controllers.Singleton<Player> {

        private static readonly int moveX = Animator.StringToHash("moveX");
        private static readonly int moveY = Animator.StringToHash("moveY");

        public bool FacingLeft {
            get => facingLeft;
            private set => facingLeft = value;
        }

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float dashSpeed = 4f;
        [SerializeField] float dashCooldown = 0.25f;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Transform weaponCollider;

        private bool facingLeft;
        private bool isDashing;
        private float defaultMoveSpeed;

        private Vector2 movement;
        private Rigidbody2D rb;
        private Animator anim;
        private PlayerControls playerControls;
        private SpriteRenderer sprite;
        private Camera mainCamera;

        protected override void Awake() {
            base.Awake();
            
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            playerControls = new PlayerControls();
        }

        private void Start() {
            // base.Start();
            
            playerControls.Movement.Dash.performed += _ => Dash();
            defaultMoveSpeed = moveSpeed;
        }

        private void Update() {
            PlayerInput();
        }

        private void FixedUpdate() {
            Move();
            AdjustPlayerFacing();
        }

        public Transform GetWeaponCollider() {
            return weaponCollider;
        }

        private void Move() {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
        }

        private void AdjustPlayerFacing() {
            var mousePos = Mouse.current.position.ReadValue();

            if (mainCamera != null) {
                Vector3 playerScreenPoint = mainCamera.WorldToScreenPoint(transform.position);

                if (mousePos.x < playerScreenPoint.x) {
                    sprite.flipX = true;
                    facingLeft = true;
                } else {
                    sprite.flipX = false;
                    facingLeft = false;
                }
            }
        }

        private void PlayerInput() {
            movement = playerControls.Movement.Move.ReadValue<Vector2>();
            anim.SetFloat(moveX, movement.x);
            anim.SetFloat(moveY, movement.y);
        }

        private void Dash() {
            if (!isDashing) {
                isDashing = true;
                moveSpeed *= dashSpeed;
                trailRenderer.emitting = true;
                StartCoroutine(EndDashTime());
            }
        }

        private IEnumerator EndDashTime() {
            float dashTime = 0.2f;
            yield return new WaitForSeconds(dashTime);
            moveSpeed = defaultMoveSpeed;
            trailRenderer.emitting = false;
            yield return new WaitForSeconds(dashCooldown);
            isDashing = false;
        }

        private void OnEnable() {
            playerControls.Enable();
        }
    }
}
