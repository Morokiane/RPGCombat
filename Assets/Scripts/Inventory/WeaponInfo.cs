using UnityEngine;

namespace Inventory {
    [CreateAssetMenu(menuName = "New Weapon")]

    public class WeaponInfo : ScriptableObject {
        public GameObject weaponPrefab;
        public float weaponCooldown;
        public uint weaponDamage;
        public float weaponRange;
    }
}
