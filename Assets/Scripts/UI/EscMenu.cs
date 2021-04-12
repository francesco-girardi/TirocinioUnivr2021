using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{

    public bool gamePaused = false;

    public GameObject pauseMenuUI;

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
}
