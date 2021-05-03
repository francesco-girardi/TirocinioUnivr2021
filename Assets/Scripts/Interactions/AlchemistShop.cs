using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions {

    public class AlchemistShop : MonoBehaviour {
        private bool isShopping = false;

        private PlayerLogic player;

        void Start() {

        }

        void Update() {

        }

        void OnTriggerStay(Collider other) {
            Vector3 posizione = new Vector3(18.19f, 2.79f, 17.28f);

            if (Input.GetKey(KeyCode.E) && isShopping == false) {
                isShopping = true;
            }

            if (Input.GetKey(KeyCode.Escape) && isShopping == true) {
                isShopping = false;
            }
        }



    }
}
