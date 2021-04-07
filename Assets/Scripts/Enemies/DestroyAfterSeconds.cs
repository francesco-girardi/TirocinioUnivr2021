using UnityEngine;

namespace Enemy {

    public class DestroyAfterSeconds : MonoBehaviour {

        [SerializeField]
        private float timer;

        void Start() {
            Destroy(gameObject, timer);
        }

    }

}
