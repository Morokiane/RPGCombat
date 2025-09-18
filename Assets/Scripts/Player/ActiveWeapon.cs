using UnityEngine;
using System.Collections;

namespace Player {
    public class ActiveWeapon : Controllers.Singleton<ActiveWeapon> {
        
        public MonoBehaviour currentActiveWeapon { get; private set; }

        private PlayerControls playerControls;

        private float timeBetweenAttacks;
        private bool attackButtonDown, isAttacking;

        protected override void Awake() {
            base.Awake();
            
            playerControls = new PlayerControls();
        }

        private void Start() {
            // base.Start();

            playerControls.Combat.Attack.started += _ => StartAttacking();
            playerControls.Combat.Attack.canceled += _ => StopAttacking();
            AttackCooldown();
        }

        private void OnEnable() {
            playerControls.Enable();
        }

        private void FixedUpdate() {
            Attack();
        }

        public void NewWeapon(MonoBehaviour newWeapon) {
            currentActiveWeapon = newWeapon;
            AttackCooldown();
            timeBetweenAttacks = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
        }

        public void WeaponNull() {
            currentActiveWeapon = null;
        }

        private void AttackCooldown() {
            isAttacking = true;
            StopAllCoroutines();
            StartCoroutine(TimeBetweenAttacks());
        }

        private IEnumerator TimeBetweenAttacks() {
            yield return new WaitForSeconds(timeBetweenAttacks);
            isAttacking = false;
            
        }

        private void Attack() {
            if (attackButtonDown && !isAttacking && currentActiveWeapon) {
                AttackCooldown();
                (currentActiveWeapon as IWeapon).Attack();
            }
        }

        private void StartAttacking() {
            attackButtonDown = true;
        }

        private void StopAttacking() {
            attackButtonDown = false;
        }
    }
}
