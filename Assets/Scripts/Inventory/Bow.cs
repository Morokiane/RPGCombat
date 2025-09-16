using UnityEngine;

namespace Inventory {
    public class Bow : MonoBehaviour, IWeapon {
        
        [SerializeField] private WeaponInfo weaponInfo;

        public WeaponInfo GetWeaponInfo() {
            return weaponInfo;
        }

        public void Attack() {
            Debug.Log("pew, pew");
        }
    }
}
