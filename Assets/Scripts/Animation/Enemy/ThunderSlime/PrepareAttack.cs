using UnityEngine;

namespace Animation.Enemy.ThunderSlime {

    public class PrepareAttack : MonoBehaviour {

        [SerializeField]
        private GameObject lightning;

        [SerializeField]
        private ParticleSystem discharge;

        private void ActivateLightning() {
            lightning.SetActive(true);
        }

        private void DeactivateLightning() {
            lightning.SetActive(false);
        }

        private void ActivateDischarge() {
            discharge.Play();
        }

    }

}
