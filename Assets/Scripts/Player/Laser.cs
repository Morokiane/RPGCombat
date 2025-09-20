using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Laser : MonoBehaviour {
        [SerializeField] private float laserGrowTime = 2f;

        private bool isGrowing = true;
        private float laserRange;

        private SpriteRenderer sprite;
        private CapsuleCollider2D capsuleCollider;

        private void Awake() {
            sprite = GetComponent<SpriteRenderer>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        private void Start() {
            LaserFaceMouse();
        }

        private void LaserFaceMouse() {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            if (Camera.main != null)
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 direction = transform.position - mousePos;
            transform.right = -direction;
        }

        void OnTriggerEnter2D(Collider2D _other) {
            if (_other.gameObject.GetComponent<Utils.Indestructible>() && !_other.isTrigger) {
                isGrowing = false;
            }
        }

        private IEnumerator IncreaseLaserLength() {
            float timePassed = 0f;

            while (sprite.size.x < laserRange && isGrowing) {
                timePassed += Time.deltaTime;
                float linearT = timePassed / laserGrowTime;

                sprite.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);
                // Resizes the collider
                capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider.size.y);
                capsuleCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, 0);

                yield return null;
            }

            StartCoroutine(GetComponent<Utils.SpriteFade>().SlowFade());
        }

        public void UpdateLaserRange(float _laserRange) {
            this.laserRange = _laserRange;
            StartCoroutine(IncreaseLaserLength());
        }
    }
}
