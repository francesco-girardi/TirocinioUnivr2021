using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;

namespace UI
{

    public class EscMenu : MonoBehaviour
    {
        public bool gamePaused = false;

        public GameObject pauseMenuUI;

        GameObject player;

        private void Start() {
            player = PlayerManager.Instance.playerObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (gamePaused)
                    resume();
                else
                    pause();
        }

        public void resume()
        {
            Time.timeScale = 1f;
            gamePaused = false;
            pauseMenuUI.SetActive(false);

        }

        public void pause()
        {
            Time.timeScale = 0f;
            gamePaused = true;
            pauseMenuUI.SetActive(true);

        }

        public void goToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }


        /// <summary>
        /// Save current player datas
        /// </summary>
        public void Save(){

            PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            Transform playerTransform = player.transform;
            int health = playerLogic.currentHealth;
            int money = playerLogic.playerWallet.GetMoney();

            PlayerDatas playerDatas = new PlayerDatas(sceneIndex, playerTransform, health, money);

            Debug.Log("EscMenu :: Saving datas.");
            SavingSystem.PlayerToJSON(playerDatas, playerLogic.DataPath);
            
        }

    }
}
