using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEditorAttribute;
using Data;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Scene]
        public string town;
        public void NewGame()
        {
            // PlayerDatas playerDatas = new PlayerDatas(100, 0);
            // SavingSystem.PlayerToJSON(playerDatas, DataPath);
            SceneManager.LoadScene(town);
        }

        public void Continue()
        {

        }

        public void Quit()
        {   
            Debug.Log("STO USCENDO FIGLI DI PUTTANAAAAAAAA");
            Application.Quit();
        }
    }
}
