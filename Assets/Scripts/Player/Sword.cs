using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Player {
    public class Sword : MonoBehaviour {
        private static readonly int attack = Animator.StringToHash("Attack");

        [SerializeField] private GameObject slash;
        [SerializeField] private Transform hurtBox;
        [SerializeField] private float swordAttackTime = 0.5f;

        private bool attackButtonDown, isAttacking;

        private PlayerControls playerControls;
        private Animator anim;
        private Player player;
        private ActiveWeapon activeWeapon;
        private Camera mainCamera;
        private GameObject slashObject;

        private void Awake() {
            playerControls = new PlayerControls();
            anim = GetComponent<Animator>();
            player = GetComponentInParent<Player>();
            activeWeapon = GetComponentInParent<ActiveWeapon>();
        }

        private void Start() {
            mainCamera = Camera.main;
            playerControls.Combat.Attack.started += _ => StartAttacking();
            playerControls.Combat.Attack.canceled += _ => StopAttacking();
            hurtBox.gameObject.SetActive(false);
        }

        private void Update() {
            MouseFollowWithOffset();
        }

        private void FixedUpdate() {
            Attack();
        }

        private void StartAttacking() {
            attackButtonDown = true;
        }

        private void StopAttacking() {
            attackButtonDown = false;
        }

        private void Attack() {
            if (attackButtonDown && !isAttacking) {
                isAttacking = true;
                anim.SetTrigger(attack);
                slashObject = Instantiate(slash, transform.position, Quaternion.identity);
                hurtBox.gameObject.SetActive(true);
                StartCoroutine(AttackCooldown());
            }
        }

        private IEnumerator AttackCooldown() {
            yield return new WaitForSeconds(swordAttackTime);
            isAttacking = false;
        }

        /// <summary>
        /// These are called via the Animator
        /// </summary>
        public void SwingUpAnim() {
            slashObject.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

            if (player.FacingLeft) {
                slashObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void SwingDownAnim() {
            slashObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (player.FacingLeft) {
                slashObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void WeaponColliderOff() {
            hurtBox.gameObject.SetActive(false);
        }
        
        //**********

        private void MouseFollowWithOffset() {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            // Use these if the sword following the mouse is desired
            // Vector2 playerPos = player.transform.position;
            // float angle = Mathf.Atan2(mousePos.y - playerPos.y, Mathf.Abs(mousePos.x - playerPos.x)) * Mathf.Rad2Deg;
            
            if (mainCamera != null) {
                Vector3 mouseWorldPos = mainCamera.WorldToScreenPoint(player.transform.position);

                if (mousePos.x < mouseWorldPos.x) {
                    activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
                    hurtBox.transform.rotation = Quaternion.Euler(0, -180, 0);
                } else {
                    activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
                    hurtBox.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        private void OnEnable() {
            playerControls.Enable();
        }
    }
}
