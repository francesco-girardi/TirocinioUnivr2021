using UnityEngine;

namespace Interactions.Items {

    public class Item : ScriptableObject {

        [Header("Item Info")]
        [Tooltip("Item name")]
        new public string name = "New Item";

        [Tooltip("Item icon source")]
        public Sprite icon = null;

        /// <summary>
        /// Called when player use a item
        /// </summary>
        public virtual void Use() {
            Debug.Log("Using " + name);
        }

    }

}
