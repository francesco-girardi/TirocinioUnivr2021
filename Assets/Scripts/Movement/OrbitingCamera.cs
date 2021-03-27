using System.Collections;
using UnityEngine;

namespace Movement {

    public class OrbitingCamera : MonoBehaviour {
        private Transform target;

        public float distance = 5f;

        public float sensitivity = 100f;

        private float yRot = 0f;

        private float xRot = 20f;

        Vector2 distanceMinMax;

        private void Awake() {
            StartCoroutine(targetPlayer());
        }

        private IEnumerator targetPlayer() {
            yield return new WaitForSeconds(1);

            target = FindObjectOfType<PlayerLogic>().transform;
        }

        private void Start() {
            distanceMinMax = new Vector2(0.5f, distance);

#if UNITY_EDITOR
            // Somehow after updating to 2019.3, mouse axes sensitivity decreased, but only in the editor.
            sensitivity *= 5f;
#elif UNITY_WEBGL
            // To prevent the mouse axes not being detected when the cursor leaves the game window.
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        private void LateUpdate() {
            if (target == null)
                return;

            yRot += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            xRot -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            xRot = Mathf.Clamp(xRot, -75f, 75f);

            Quaternion worldRotation = transform.parent != null ? transform.parent.rotation : Quaternion.FromToRotation(Vector3.up, target.up);
            Quaternion cameraRotation = worldRotation * Quaternion.Euler(xRot, yRot, 0f);
            // Quaternion cameraRotation = Quaternion.Euler(xRot, yRot, 0f);
            Vector3 targetToCamera = cameraRotation * new Vector3(0f, 0f, -distanceMinMax.y);
            Vector3 desiredPosition = target.position + targetToCamera;
            Vector3 position = target.position + cameraRotation * new Vector3(0f, 0f, 0f);
            if (Physics.Linecast(position, desiredPosition, out RaycastHit hit))
                distance = Mathf.Clamp(hit.distance, distanceMinMax.x, distanceMinMax.y);
            else
                distance = distanceMinMax.y;

            position = target.position + cameraRotation * new Vector3(0f, 0f, -distance);

            transform.SetPositionAndRotation(position, cameraRotation);
        }
    }
}
