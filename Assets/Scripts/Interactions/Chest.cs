using UnityEngine;

namespace Interactions {

    public class Chest : Interactable {

        public override void Interact() {
            Debug.Log("Opening chest...");
        }

        public override void DoOnUpdate() {
            Debug.Log("Chest on Update...");
        }

    }

}
