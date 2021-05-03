using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions {

    public class AlchemistShop : MonoBehaviour {
        private bool isShopping = false;

        private PlayerLogic player;

        public GameObject alchimista;
        public GameObject maschere;
        public GameObject suora;
        public GameObject fabbro;

        void Start() {

        }

        void Update() {

        }

        void OnTriggerStay(Collider other) {
            Vector3 posizione = new Vector3(18.19f, 2.79f, 17.28f);

            if (Input.GetKey(KeyCode.E) && isShopping == false) {
                switch (this.tag) {
                    case "Alchimista":
                        Alchimista();
                        break;
                    case "Fabbro":
                        Fabbro();
                        break;
                    case "Suora":
                        Suora();
                        break;
                    case "Maschere":
                        Maschere();
                        break;
                    default:
                        Debug.LogError("Tag non presente");
                        break;
                }
                isShopping = true;
            }

            if (Input.GetKey(KeyCode.Escape) && isShopping == true) {
                CloseCanvas();
                isShopping = false;
            }
        }

        private void Alchimista() {
            alchimista.SetActive(true);
        }
        private void Fabbro() {
            fabbro.SetActive(true);
        }
        private void Suora() {
            suora.SetActive(true);
        }
        private void Maschere() {
            maschere.SetActive(true);
        }
        private void CloseCanvas() {
            alchimista.SetActive(false);
            maschere.SetActive(false);
            fabbro.SetActive(false);
            maschere.SetActive(false);
        }
    }
}
