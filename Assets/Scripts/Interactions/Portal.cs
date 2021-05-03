using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEditorAttribute;

namespace Interactions {

    public class Portal : MonoBehaviour {

        [Header("Portal Info")]
        public float radius = 3f;

        [Scene]
        [Tooltip("New scene to load")]
        public string sceneToLoad;

        private PlayerLogic player;

        private void Start() {
            StartCoroutine(TargetPlayer());
        }

        public void Teleport() {
            float distance = float.MaxValue;

            if (player != null)
                distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= radius) {
                SceneManager.LoadScene(sceneToLoad);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private IEnumerator TargetPlayer() {
            yield return new WaitForSeconds(1);

            player = PlayerManager.Instance.playerObject.GetComponent<PlayerLogic>();
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }

}
