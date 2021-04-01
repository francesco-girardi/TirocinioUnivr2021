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

        private Transform player;

        private void Start() {
            StartCoroutine(TargetPlayer());
        }

        public void Teleport() {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius) {
                SceneManager.LoadScene(sceneToLoad);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private IEnumerator TargetPlayer() {
            yield return new WaitForSeconds(1);

            player = PlayerManager.Instance.playerObject.transform;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }

}
