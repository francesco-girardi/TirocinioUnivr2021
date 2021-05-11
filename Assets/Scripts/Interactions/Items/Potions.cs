using System.Collections;
using UnityEngine;

namespace Interactions.Items {

    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Potion")]
    public class Potions : Item {

        /// <summary>
        /// Potion duration time
        /// </summary>
        [Header("Potion Info")]
        [Tooltip("Potion duration time")]
        public float Effect_duration = 0f;
        public float cd = 10f;
        public int healthreg = 10;

        private PlayerLogic player;
        public override void Use() {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
            player.RegenHealth(healthreg);
        }
    }


}
