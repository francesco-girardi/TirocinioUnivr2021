using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace VFX {

    public class SwordChange : MonoBehaviour{

        [SerializeField]
        private MeshRenderer Sword;

        [SerializeField]
        private Material BaseMat;

        [SerializeField]
        private Material ActiveMat;

        bool isActive = false;

        [SerializeField]
        private GameObject visualEffect;

        void Start() {
            // InvokeRepeating("ChangeStance", 2.0f, 10.0f);
        }

        public void Update() {

            if(SongController.Instance.IsPeak)
                ToggleOn();
            else
                ToggleOff();
        }

        void ChangeStance() {
            if (isActive){
                Sword.material = ActiveMat;
                visualEffect.SetActive(true);
                isActive = false;
            }
            else{
                Sword.material = BaseMat;
                visualEffect.SetActive(false);
                isActive = true;
            }
        }

        void ToggleOn() {
            Sword.material = ActiveMat;
            visualEffect.SetActive(true);
            isActive = true;
        }

        void ToggleOff(){
            Sword.material = BaseMat;
            visualEffect.SetActive(false);
            isActive = false;
        }
    }
}
