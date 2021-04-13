using Interactions.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Inventory {

    public class Inventory : MonoBehaviour {

        public static Inventory Instance { get; private set; }

        public List<Item> items = new List<Item>();

        private int space = int.MaxValue - 1;

        public bool AddItem(Item item) {

            if (space >= items.Count) {
                Debug.Log("Not enought room.");
                return false;
            }

            items.Add(item);

            return true;
        }

        public void RemoveItem(Item item) {
            items.Remove(item);
        }

        private void Awake() {
            #region Singleton
            if (Instance == null)
                Instance = this;
            else {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            #endregion
        }

    }

}
