using System.Collections;
using System.Collections.Generic;
using Animation.Player;
using Audio;
using UnityEngine;

namespace VFX {

    public class SwordChange : MonoBehaviour {


        [SerializeField]
        private MeshRenderer Sword;

        [SerializeField]
        private Material BaseMat;

        [SerializeField]
        private Material ActiveMat;

        [SerializeField]
        private GameObject visualEffect;

        private PlayerAnimator animator;

        private PlayerManager player;

        void Start() {
            player = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
            // InvokeRepeating("ChangeStance", 2.0f, 10.0f);
            StartCoroutine(GetPlayer());

        }

        private IEnumerator GetPlayer() {
            yield return new WaitForSeconds(0.5f);
            animator = player.playerObject.GetComponentInChildren<PlayerAnimator>();
        }

        public void Update() {

            if (animator != null) {
                if (SongController.Instance.IsPeak && animator.Attacked)
                    ToggleOn();
                if (!animator.StartedAttack)
                    ToggleOff();
            }
        }

        // void ChangeStance() {
        //     if (isActive) {
        //         Sword.material = ActiveMat;
        //         visualEffect.SetActive(true);
        //         isActive = false;
        //     }
        //     else {
        //         Sword.material = BaseMat;
        //         visualEffect.SetActive(false);
        //         isActive = true;
        //     }
        // }

        void ToggleOn() {
            Sword.material = ActiveMat;
            if (visualEffect != null)
                visualEffect.SetActive(true);
        }

        void ToggleOff() {
            Sword.material = BaseMat;
            if (visualEffect != null)
                visualEffect.SetActive(false);
        }
    }
}
