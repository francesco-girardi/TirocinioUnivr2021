using System.Collections;
using UnityEngine;

namespace Stat {

    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour {

        public event System.Action OnAttack;

        private CharacterStats myStats;

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

#if UNITY_EDITOR
                Debug.Log(targetStats.name + " takes damage: " + myStats.damage.GetValue());
#endif
            }
        }

        private IEnumerator DoDamage(CharacterStats targetStats, float delay) {
            yield return new WaitForSeconds(delay);

            targetStats.TakeDamage(myStats.damage.GetValue());
        }

        private void Start() {
            myStats = GetComponent<CharacterStats>();
        }

        private void Update() {
            myStats.attackCooldown -= Time.deltaTime;
        }

    }

}
