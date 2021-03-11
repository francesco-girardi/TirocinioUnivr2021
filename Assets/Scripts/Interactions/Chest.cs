using UnityEngine;

namespace Interactions {

    public class Chest : Interactable {

        [Tooltip("UI elemt to show when chest is open")]
        [Header("UIElement")]
        public GameObject chestUI;

        public override void Interact() {
            Debug.Log("Opening chest...");
            Cursor.lockState = CursorLockMode.None;
            chestUI.SetActive(true);
        }

        public override void DoOnUpdate() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("Closing chest...");
                Cursor.lockState = CursorLockMode.None;
                chestUI.SetActive(false);
                OnDefocused();
            }
        }

    }

}
