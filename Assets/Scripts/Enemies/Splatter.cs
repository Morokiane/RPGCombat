using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies {
    public class Splatter : MonoBehaviour {
        private Utils.SpriteFade spriteFade;

        private void Awake() {
            spriteFade = GetComponent<Utils.SpriteFade>();
        }

        private void Start() {
            StartCoroutine(spriteFade.SlowFade());
            
            Invoke(nameof(DisableCollider), 0.2f);
        }

        private void OnTriggerEnter2D(Collider2D _other) {
            PlayerHealth playerHealth = _other.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(1, transform);
        }

        private void DisableCollider() {
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
