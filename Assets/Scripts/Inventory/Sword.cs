using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Inventory {
    public class Sword : MonoBehaviour, IWeapon {
        private static readonly int attack = Animator.StringToHash("Attack");

        [SerializeField] private GameObject slash;
        [SerializeField] private float swordAttackTime = 0.5f;

        private Animator anim;
        // private Player.Player player;
        // private Player.ActiveWeapon activeWeapon;
        private Camera mainCamera;
        private GameObject slashObject;
        private Transform weaponCollider;

        private void Awake() {
            anim = GetComponent<Animator>();
            // player = GetComponentInParent<Player.Player>();
            // activeWeapon = GetComponentInParent<Player.ActiveWeapon>();
        }

        private void Start() {
            mainCamera = Camera.main;
            weaponCollider = Player.Player._instance.GetWeaponCollider();
            weaponCollider.gameObject.SetActive(false);
        }

        private void Update() {
            MouseFollowWithOffset();
        }

        public void Attack() {
            // isAttacking = true;
            anim.SetTrigger(attack);
            slashObject = Instantiate(slash, transform.position, Quaternion.identity);
            weaponCollider.gameObject.SetActive(true);
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown() {
            yield return new WaitForSeconds(swordAttackTime);
            Player.ActiveWeapon._instance.ToggleIsAttacking(false);
        }

        /// <summary>
        /// These are called via the Animator
        /// </summary>
        public void SwingUpAnim() {
            slashObject.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

            if (Player.Player._instance.FacingLeft) {
                slashObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void SwingDownAnim() {
            slashObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Player.Player._instance.FacingLeft) {
                slashObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void WeaponColliderOff() {
            weaponCollider.gameObject.SetActive(false);
        }
        
        //**********

        private void MouseFollowWithOffset() {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            // Use these if the sword following the mouse is desired
            // Vector2 Player.Player._instance.os = Player.Player._instance.transform.position;
            // float angle = Mathf.Atan2(mousePos.y - Player.Player._instance.os.y, Mathf.Abs(mousePos.x - Player.Player._instance.os.x)) * Mathf.Rad2Deg;
            
            if (mainCamera != null) {
                Vector3 mouseWorldPos = mainCamera.WorldToScreenPoint(Player.Player._instance.transform.position);

                if (mousePos.x < mouseWorldPos.x) {
                    Player.ActiveWeapon._instance.transform.rotation = Quaternion.Euler(0, -180, 0);
                    weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
                } else {
                    Player.ActiveWeapon._instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

    }
}
