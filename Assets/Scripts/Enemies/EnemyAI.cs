using UnityEngine;
using System.Collections;

namespace Enemies {
    public class EnemyAI : MonoBehaviour {
        private enum State {
            Roaming
        }

        [SerializeField] private float roam = 2f;

        private State state;
        private EnemyPathFinding enemyPathFinding;

        private void Awake() {
            enemyPathFinding = GetComponent<EnemyPathFinding>();
            state = State.Roaming;
        }

        private void Start() {
            StartCoroutine(RoamingRoutine());
        }

        private IEnumerator RoamingRoutine() {
            while (state == State.Roaming) {
                Vector2 roamPosition = GetRoamingPosition();
                enemyPathFinding.MoveTo(roamPosition);
                yield return new WaitForSeconds(roam);
            }
        }

        private Vector2 GetRoamingPosition() {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
