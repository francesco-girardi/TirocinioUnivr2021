using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour {

    private PlayerManager playerManager;

    private string dataPath;

    private void Awake() {
        dataPath = Application.persistentDataPath + "/playerData.json";
        PlayerDatas playerDatas = SavingSystem.PlayerFromJSON(dataPath);

        if (MainMenu.continueGame) {
            gameObject.transform.position = playerDatas.playerPosition.position;
            MainMenu.continueGame = false;
        }

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SetSpawnLocation(gameObject);
    }

    private void Start() {
        playerManager.SpawnPlayer();
    }

}
