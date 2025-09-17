using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory {
    public class Staff : MonoBehaviour, IWeapon {
        readonly int attack = Animator.StringToHash("Attack");
        
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject laser;
        [SerializeField] private Transform spawnPoint;

        private Animator anim;
        private Camera mainCamera;

        private void Awake() {
            anim = GetComponent<Animator>();
        }

        private void Start() {
            mainCamera = Camera.main;
        }
    
        private void Update() {
            MouseFollowWithOffset();
        }
        
        public WeaponInfo GetWeaponInfo() {
            return weaponInfo;
        }

        public void Attack() {
            anim.SetTrigger(attack);
        }

        public void SpawnLaser() {
            GameObject newLaser = Instantiate(laser, spawnPoint.position, Quaternion.identity);
        }

        private void MouseFollowWithOffset() {
            Vector3 mousePos = Mouse.current.position.ReadValue();

            if (mainCamera != null) {
                Vector3 playerScreenPoint = mainCamera.WorldToScreenPoint(Player.Player._instance.transform.position);
                float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            
                if (mousePos.x < playerScreenPoint.x) {
                    Player.ActiveWeapon._instance.transform.rotation = Quaternion.Euler(0, -180, angle);
                } else {
                    Player.ActiveWeapon._instance.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }
    }
}
