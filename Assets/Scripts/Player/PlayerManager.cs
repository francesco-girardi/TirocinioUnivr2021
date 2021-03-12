using UnityEngine;

public class PlayerManager : MonoBehaviour {

    /// <summary>
    /// Player instance
    /// </summary>
    public static PlayerManager Instance;

    /// <summary>
    /// Player object
    /// </summary>
    public GameObject playerObject;

    private void Awake() {
        Instance = this;
    }

}
