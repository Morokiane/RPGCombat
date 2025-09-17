using UnityEngine;

namespace Inventory {
    public class Bow : MonoBehaviour, IWeapon {
        readonly int fire = Animator.StringToHash("Fire");
        
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform arrowSpawnPoint;

        private Animator anim;

        private void Awake() {
            anim = GetComponent<Animator>();
        }

        public void Attack() {
            anim.SetTrigger(fire);
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Player.ActiveWeapon._instance.transform.rotation);
            newArrow.GetComponent<Utils.Projectile>().UpdateWeaponInfo(weaponInfo);
        }
        
        public WeaponInfo GetWeaponInfo() {
            return weaponInfo;
        }
    }
}
