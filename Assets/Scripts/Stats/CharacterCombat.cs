using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stat {

    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour {

        private CharacterStats myStats;

        /// <summary>
        /// Attack other characters in game
        /// </summary>
        /// <param name="targetStats"></param>
        public void Attack(CharacterStats targetStats) {
            targetStats.TakeDamage(myStats.damage.GetValue());
        }

        private void Start() {
            myStats = GetComponent<CharacterStats>();
        }

    }

}
