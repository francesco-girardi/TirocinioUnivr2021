using UnityEngine;

namespace Animation.Player {

    public class SwordBuff : MonoBehaviour {

        [SerializeField]
        private MeshRenderer Sword;

        [SerializeField]
        private Material BaseMat;

        [SerializeField]
        private Material ActiveMat;

        bool isActive = false;

        // [SerializeField]
        // private GameObject visualEffect;

        private void Start() {
            InvokeRepeating("ChangeStance", 2.0f, 2.0f);
        }

        private void ChangeStance() {
            if (isActive) {
                Sword.material = ActiveMat;
                // visualEffect.SetActive(true);
                isActive = false;
            } else {
                Sword.material = BaseMat;
                // visualEffect.SetActive(false);
                isActive = true;
            }
        }

    }

}
