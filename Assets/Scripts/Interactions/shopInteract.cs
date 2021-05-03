using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Interactions {

    public class shopInteract : MonoBehaviour {
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
                EscMenu.GamePaused = true;
                Cursor.lockState = CursorLockMode.None;
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
                Cursor.lockState = CursorLockMode.Locked;
                isShopping = false;
                EscMenu.GamePaused = false;
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
            if (alchimista != null)
                alchimista.SetActive(false);
            if (maschere != null)
                maschere.SetActive(false);
            if (fabbro != null)
                fabbro.SetActive(false);
            if (maschere != null)
                maschere.SetActive(false);
        }
    }
}
