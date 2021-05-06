using Interactions.Items;
using UnityEngine;

namespace UI {
    public class ShopItem : MonoBehaviour {

        public Item item;

        [HideInInspector]
        public int itemCount;

        public void AddItem() {
            itemCount++;

            TMPro.TMP_Text text = gameObject.GetComponentInChildren<TMPro.TMP_Text>();
            if (text != null)
                text.text = itemCount.ToString();

            if (itemCount > 99)
                Debug.Log("You can't add more");
        }

        public void RemoveItem() {
            itemCount--;

            TMPro.TMP_Text text = gameObject.GetComponentInChildren<TMPro.TMP_Text>();
            if (text != null)
                text.text = itemCount.ToString();
        }

    }
}