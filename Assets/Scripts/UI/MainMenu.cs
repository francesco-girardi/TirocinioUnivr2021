using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEditorAttribute;
using Data;

namespace UI {
    public class MainMenu : MonoBehaviour {

        public static bool continueGame = false;

        [Scene]
        public string town;

        private string dataPath;

        private void Awake() {
            dataPath = Application.persistentDataPath + "/playerData.json";
        }

        public void NewGame() {
            PlayerDatas playerDatas = new PlayerDatas(1, Vector3.zero, Quaternion.Euler(0, 0, 0), 100, 0);
            SavingSystem.PlayerToJSON(playerDatas, dataPath);
            SceneManager.LoadScene(town);
        }

        public void Continue() {
            continueGame = true;
            PlayerDatas playerDatas = SavingSystem.PlayerFromJSON(dataPath);
            SceneManager.LoadSceneAsync(playerDatas.sceneBuildIndex);
        }

        public void Quit() {
            Debug.Log("STO USCENDO FIGLI DI PUTTANAAAAAAAA");
            Application.Quit();
        }
    }
}
