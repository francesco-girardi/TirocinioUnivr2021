using UnityEngine;
using UnityEngine.SceneManagement;
using Data;
using Audio;
using Console;

namespace UI {

    public class EscMenu : MonoBehaviour {

        private static bool gamePaused = false;

        public static bool GamePaused {
            get => gamePaused;
        }

        public GameObject pauseMenuUI;
        public GameObject onScreenUI;

        private string dataPath;

        GameObject player;

        private AudioSource audioSource;

        private void Awake() {
            dataPath = Application.persistentDataPath + "/playerData.json";
        }

        // Update is called once per frame
        void Update() {

            audioSource = SoundManager.Instance.backgroundMusic;

            if (Input.GetKeyDown(KeyCode.Escape))
                if (gamePaused)
                    Resume();
                else
                    Pause();
        }

        public void Resume() {
            Time.timeScale = 1f;
            audioSource.Play();
            Cursor.lockState = CursorLockMode.Locked;
            gamePaused = false;
            pauseMenuUI.SetActive(false);
            onScreenUI.SetActive(true);

        }

        public void Pause() {
            Time.timeScale = 0f;
            audioSource.Pause();
            Cursor.lockState = CursorLockMode.None;
            gamePaused = true;
            pauseMenuUI.SetActive(true);
            onScreenUI.SetActive(false);
            DebugController.ShowConsole = false;
        }

        public void GoToMainMenu() {
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
            Resume();

        }

    }
}
