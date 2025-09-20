using System.Collections;
using UnityEngine;

namespace Enemies {
    public class EnemyAI : MonoBehaviour {
        private enum State {
            Roaming,
            Attacking
        }

        [SerializeField] private float roam = 2f;
        [SerializeField] private float attackRange = 0f;
        [SerializeField] private MonoBehaviour enemyType;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private bool stopMovingWhenAttacking;

        private State state;
        private EnemyPathFinding enemyPathFinding;
        private bool canAttack = true;
        private Vector2 roamPosition;
        private float timeRoaming = 0f;

        private void Awake() {
            enemyPathFinding = GetComponent<EnemyPathFinding>();
            state = State.Roaming;
        }

        private void Start() {
            roamPosition = GetRoamingPosition();
        }

        private void Update() {
            MovementStateControl();
        }

        private void MovementStateControl() {
            switch (state) {
                default:
                case State.Roaming:
                    Roaming();
                    break;
                case State.Attacking:
                    Attacking();
                    break;
            }
        }

        private void Roaming() {
            timeRoaming += Time.deltaTime;

            enemyPathFinding.MoveTo(roamPosition);

            if (Vector2.Distance(transform.position, Player.Player._instance.transform.position) < attackRange) {
                state = State.Attacking;
            }

            if (timeRoaming > roam) {
                roamPosition = GetRoamingPosition();
            }
        }

        private void Attacking() {

            if (Vector2.Distance(transform.position, Player.Player._instance.transform.position) > attackRange) {
                state = State.Roaming;
            }

            // If I'm crashing to here...make sure melee enemies have no attack range
            if (attackRange != 0 && canAttack) {
                canAttack = false;
                (enemyType as IEnemy).Attack();

                if (stopMovingWhenAttacking) {
                    enemyPathFinding.StopMoving();
                } else {
                    enemyPathFinding.MoveTo(roamPosition);
                }

                StartCoroutine(AttackCooldown());
            }
        }

        private IEnumerator AttackCooldown() {
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        private Vector2 GetRoamingPosition() {
            timeRoaming = 0f;
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
