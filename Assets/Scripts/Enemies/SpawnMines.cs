using UnityEngine;

namespace Enemy {

    public class SpawnMines : MonoBehaviour {

        private float cooldown;

        [SerializeField]
        private float startCooldown;

        [SerializeField]
        private GameObject Mina;

        [SerializeField]
        private GameObject SpawnPoint;

        void Start() {
            cooldown = startCooldown;
        }

        void Update() {
            if (cooldown <= 0) {
                Instantiate(Mina, SpawnPoint.transform.position, Quaternion.identity);
                cooldown = startCooldown;
            } else {
                cooldown -= Time.deltaTime;
            }
        }

    }

}

