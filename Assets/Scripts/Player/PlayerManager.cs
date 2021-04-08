using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    /// <summary>
    /// Player instance
    /// </summary>
    public static PlayerManager Instance { get; private set; }

    /// <summary>
    /// Player object
    /// </summary>
    [Header("Player Info")]
    [Tooltip("Player object to spawn")]
    public GameObject playerToSpawnObject;

    [HideInInspector]
    public GameObject playerObject;

    private GameObject spawnLocation;

    /// <summary>
    /// Called when player died
    /// </summary>
    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.PlaySound("explosion_01");
    }

    public void SetSpawnLocation(GameObject spawnLocation) {
        this.spawnLocation = spawnLocation;
    }

    /// <summary>
    /// Spawn a Player in the scene
    /// </summary>
    public void SpawnPlayer() {
        Instance.playerToSpawnObject.transform.name = "Player";
        playerObject = Instantiate(Instance.playerToSpawnObject, Instance.spawnLocation.transform.position,
            Instance.spawnLocation.transform.rotation);
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

        spawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
    }

}
