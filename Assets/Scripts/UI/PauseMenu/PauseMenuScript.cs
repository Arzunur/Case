using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    private bool isGamePaused = false;

    private void Start()
    {
        if (pausePanel)
            pausePanel.SetActive(false);

        Time.timeScale = 1;
    }
    public void TogglePausePanel()
    {
        isGamePaused = !isGamePaused;
        pausePanel.SetActive(isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void ContinueGame()
    {
        isGamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        ResumeTime();
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void Quit()
    {
         Application.Quit();
    }

    private void ResumeTime()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void PausePanelOpen()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}


