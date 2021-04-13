using UnityEngine;
using UnityEngine.SceneManagement;
using Data;

namespace UI {

    public class EscMenu : MonoBehaviour {
        public bool gamePaused = false;

        public GameObject pauseMenuUI;

        private string dataPath;

        GameObject player;

        private void Awake() {
            dataPath = Application.persistentDataPath + "/playerData.json";
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (gamePaused)
                    resume();
                else
                    pause();
        }

        public void resume() {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            gamePaused = false;
            pauseMenuUI.SetActive(false);

        }

        public void pause() {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            gamePaused = true;
            pauseMenuUI.SetActive(true);

        }

        public void goToMainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }


        /// <summary>
        /// Save current player datas
        /// </summary>
        public void Save() {

            player = PlayerManager.Instance.playerObject;

            PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            Vector3 playerPosition = player.transform.position;
            Quaternion playerRotation = player.transform.rotation;
            int health = playerLogic.currentHealth;
            // int money = playerLogic.playerWallet.GetMoney();

            PlayerDatas playerDatas = new PlayerDatas(sceneIndex, playerPosition, playerRotation, health, 0);
            Debug.Log(playerDatas);
            Debug.Log("EscMenu :: Saving datas.");
            SavingSystem.PlayerToJSON(playerDatas, dataPath);
            resume();

        }

    }
}
