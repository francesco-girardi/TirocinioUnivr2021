using UnityEngine;

namespace Friends.Alchemist {

    public class AlchemistController : Friends {

        public override void Interact() {
            base.Interact();

            Debug.Log("Alchimista");
        }
    }

}