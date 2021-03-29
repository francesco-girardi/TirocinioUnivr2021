using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    private PlayerManager playerManager;

    private void Awake() {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SetSpawnLocation(gameObject);
    }

    private void Start() {
        playerManager.SpawnPlayer();
    }

}
