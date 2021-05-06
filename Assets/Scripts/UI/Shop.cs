using Interactions.Inventory;
using UnityEngine;

namespace UI {
    public class Shop : MonoBehaviour {

        private Inventory inventory;

        public ShopItem[] shopItems;

        private void Awake() {
            inventory = FindObjectOfType<Inventory>();
        }

        public void BuyImes() {
            foreach (var item in shopItems) {
                for (int i = 0; i < item.itemCount; i++)
                    inventory.AddItem(item.item);
            }
        }

    }
}