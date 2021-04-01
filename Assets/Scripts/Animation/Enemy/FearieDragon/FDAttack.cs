using UnityEngine;

namespace Animation.Enemy.FaerieDragon {

    public class FDAttack : MonoBehaviour {

        [SerializeField]
        private GameObject Ball;

        private void ChargeSphere() {
            Instantiate(Ball, transform.position, Quaternion.identity);
        }

    }

}
