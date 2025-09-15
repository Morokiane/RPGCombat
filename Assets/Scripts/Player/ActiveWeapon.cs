using UnityEngine;

namespace Player {
    public class ActiveWeapon : Controllers.Singleton<ActiveWeapon> {
        
        public MonoBehaviour currentActiveWeapon { get; private set; }

        private PlayerControls playerControls;

        private bool attackButtonDown, isAttacking;

        protected override void Awake() {
            base.Awake();
            
            playerControls = new PlayerControls();
        }

        private void Start() {
            // base.Start();

            playerControls.Combat.Attack.started += _ => StartAttacking();
            playerControls.Combat.Attack.canceled += _ => StopAttacking();
        }

        private void OnEnable() {
            playerControls.Enable();
        }

        private void FixedUpdate() {
            Attack();
        }

        public void NewWeapon(MonoBehaviour newWeapon) {
            currentActiveWeapon = newWeapon;
        }

        public void WeaponNull() {
            currentActiveWeapon = null;
        }

        private void Attack() {
            if (attackButtonDown && !isAttacking && currentActiveWeapon) {
                isAttacking = true;
                (currentActiveWeapon as IWeapon).Attack();
            }
        }

        public void ToggleIsAttacking(bool value) {
            isAttacking = value;
        }

        private void StartAttacking() {
            attackButtonDown = true;
        }

        private void StopAttacking() {
            attackButtonDown = false;
        }
    }
}
