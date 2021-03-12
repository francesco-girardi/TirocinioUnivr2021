using UnityEngine;
using UnityEngine.AI;

namespace Enemy {

    public class EnemyController : MonoBehaviour {

        /// <summary>
        /// Enemy view distance
        /// </summary>
        [Header("Enemy properties")]
        [Tooltip("Enemy view distance")]
        public float lookRadius = 10;

        private Transform target;

        private NavMeshAgent agent;

        private void Start() {
            target = PlayerManager.Instance.playerObject.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius) {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance) {
                    // TODO Attack player
                    FaceTarget();
                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }

        private void FaceTarget() {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

    }

}
