using Interactions;
using UnityEngine;

namespace Enemy {

    public class FlyEnemy : Interactable {

        public override void Interact() {
            base.Interact();
            Debug.Log("Interaction");
        }

    }

}
