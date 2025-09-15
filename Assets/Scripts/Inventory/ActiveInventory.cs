using UnityEngine;

namespace Inventory {
    public class ActiveInventory : MonoBehaviour {
        private int activeSlotIndexNum = 0;

        private PlayerControls playerControls;

        private void Awake() {
            playerControls = new PlayerControls();
        }

        private void Start() {
            playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());

            ToggleActiveHighlight(0);
        }

        private void OnEnable() {
            playerControls.Enable();
        }

        private void ToggleActiveSlot(int numValue) {
            ToggleActiveHighlight(numValue - 1);
        }

        private void ToggleActiveHighlight(int indexNum) {
            activeSlotIndexNum = indexNum;

            foreach (Transform inventorySlot in transform) {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }

            transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

            ChangeActiveWeapon(); 
        }

        private void ChangeActiveWeapon() {
            if (Player.ActiveWeapon._instance.currentActiveWeapon != null) {
                Destroy(Player.ActiveWeapon._instance.currentActiveWeapon.gameObject);
            }
            
            if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>()) {
                Player.ActiveWeapon._instance.WeaponNull();
                return;
            }
            
            GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

            GameObject newWeapon = Instantiate(weaponToSpawn, Player.ActiveWeapon._instance.transform.position, Quaternion.identity);
            newWeapon.transform.parent = Player.ActiveWeapon._instance.transform;

            Player.ActiveWeapon._instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }
    }
}
