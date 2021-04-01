using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEditorAttribute;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Scene]
        public string town;
        public void NewGame()
        {
            SceneManager.LoadScene(town);
            // SceneManager.LoadScene("Town");
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
