using UnityEngine;

/// <summary>
/// <br>This component is for all objects that the player can</br>
/// <br>interact with such as enemies, items etc. It is meant</br>
/// <br>to be used as a base class.</br>
/// </summary>
public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactionTransform;

    private Transform player;

    private bool isFocus = false;
    private bool hasInteracted = false;

    public virtual void Interact() { }

    /// <summary>
    /// Called when the object starts being focused
    /// </summary>
    /// <param name="playerTransform"></param>
    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    /// <summary>
    /// Called when the object is no longer focused
    /// </summary>
    public void OnDefocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void Update() {
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius) {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}