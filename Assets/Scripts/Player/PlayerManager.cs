using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    /// <summary>
    /// Player instance
    /// </summary>
    public static PlayerManager Instance;

    /// <summary>
    /// Player object
    /// </summary>
    public GameObject playerObject;

    /// <summary>
    /// Called when player died
    /// </summary>
    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake() {
        Instance = this;
    }

}
