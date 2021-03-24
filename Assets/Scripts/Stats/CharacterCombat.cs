using Enemy;
using System.Collections;
using UnityEngine;

namespace Stat {

    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour {

        /// <summary>
        /// Set true when character are in combat
        /// </summary>
        public bool InComabt { get; private set; }

        public event System.Action OnAttack;

        private const float combatCalldown = 5;

        private float lastAttackTime;

        private CharacterStats myStats;

        private EnemyController enemyController;

        /// <summary>
        /// Attack other characters in game
        /// </summary>
        /// <param name="targetStats"></param>
        public void Attack(CharacterStats targetStats) {
            if (myStats.attackCooldown <= 0) {
                StartCoroutine(DoDamage(targetStats, myStats.attackDelay));

                if (OnAttack != null)
                    OnAttack();

                myStats.attackCooldown = 1f / myStats.attackSpeed;

                InComabt = true;
                lastAttackTime = Time.time;
            }
        }

        private IEnumerator DoDamage(CharacterStats targetStats, float delay) {
            yield return new WaitForSeconds(delay);

            if (enemyController != null && enemyController.canDoDamage) {
                targetStats.TakeDamage(myStats.damage.GetValue());

#if UNITY_EDITOR
                Debug.Log(targetStats.name + " takes damage: " + myStats.damage.GetValue());
#endif
            } else {
                targetStats.TakeDamage(myStats.damage.GetValue());

#if UNITY_EDITOR
                Debug.Log(targetStats.name + " takes damage: " + myStats.damage.GetValue());
#endif
            }

            if (targetStats.currentHealth <= 0)
                InComabt = false;
        }

        private void Start() {
            myStats = GetComponent<CharacterStats>();

            if (gameObject.tag == "Enemy")
                enemyController = GetComponent<EnemyController>();
        }

        private void Update() {
            myStats.attackCooldown -= Time.deltaTime;

            if (Time.time - lastAttackTime > combatCalldown)
                InComabt = false;
        }

    }

}
