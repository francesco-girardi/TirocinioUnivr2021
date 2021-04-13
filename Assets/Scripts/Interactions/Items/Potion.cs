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

        public override void Use() {
            base.Use();
        }

    }

}
