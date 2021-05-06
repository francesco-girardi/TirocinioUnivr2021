using System.Collections;
using System.Collections.Generic;
using Interactions.Inventory;
using Interactions.Items;
using TMPro;
using UnityEngine;

namespace UI {
    public class Shops : MonoBehaviour {
        // private Inventory inventory;
        // public enum shopItem { Verde, Giallo, Rosa, Azzurro, Rosso };
        // public shopItem colorePozione;
        public Potions item;

        // private Dictionary<string, int> itemName = new Dictionary<string, int>();
        private int numberOfObj = 0;
        private TMP_Text testo;

        void Start() {
            testo = GetComponentInChildren<TMP_Text>();
            // inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

            // item = new Potions(colorePozione.ToString(), 1f);
        }

        void Update() {

        }

        public void IncreaseN() {
            if (numberOfObj < 99) {
                numberOfObj++;
            }
            testo.SetText(numberOfObj.ToString());
        }

        public void DecreaseN() {
            if (numberOfObj > 0) {
                numberOfObj--;
            }
            testo.SetText(numberOfObj.ToString());
        }

        public void BuyItems() {
            for (int i = 0; i < numberOfObj; i++) {
                Debug.Log("Buy " + item.name + " n° " + i);
            }
            //la funzione mette gli item nell'inventario
            numberOfObj = 0;
            testo.SetText(numberOfObj.ToString());
        }
    }
}
