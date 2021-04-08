using UnityEngine;

namespace Animation.Enemy.FaerieDragon {

    public class FDAttack : MonoBehaviour {

        [SerializeField]
        private GameObject Ball;

        [SerializeField]
        private GameObject SpawnPoint;

        private GameObject ThisBall;

        private void ChargeSphere() {
            ThisBall = Instantiate(Ball, SpawnPoint.transform.position, Quaternion.identity);
            ThisBall.GetComponent<FD_Projectile>().enabled = false;

        }
        private void Shoot() {
            ThisBall.GetComponent<FD_Projectile>().enabled = true;
        }

    }

}
