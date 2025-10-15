using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulationPanelManager : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject congratulationPanel; // tebrik paneli
 

    [Header("scene")]
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";
    private void Start()
    {
        if (congratulationPanel != null)
            congratulationPanel.SetActive(false);
    }

    private void Update()
    {
        if (levelManager != null && IsAllLevelsCompleted())
        {
            ShowCongratulationPanel();
        }
    }

    private bool IsAllLevelsCompleted()
    {
        return levelManager.GetCurrentLevelIndex() >= levelManager.GetTotalLevelCount();
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Karakter ve top atma yeniden aktif
        CharacterController character = FindObjectOfType<CharacterController>();
        if (character != null)
            character.enabled = true;

        BallThrower ball = FindObjectOfType<BallThrower>();
        if (ball != null)
            ball.EnableShooting(true);

        if (levelManager != null)
            levelManager.RestartFromFirstLevel();

        if (congratulationPanel != null)
            congratulationPanel.SetActive(false);
    }


    private void ShowCongratulationPanel()
    {
        if (congratulationPanel != null && !congratulationPanel.activeSelf)
        {
            congratulationPanel.SetActive(true);
            Time.timeScale = 0f;

            // Karakteri durdur
            CharacterController character = FindObjectOfType<CharacterController>();
            if (character != null)
                character.enabled = false;

            // Top atmayı durdur
            BallThrower ball = FindObjectOfType<BallThrower>();
            if (ball != null)
                ball.EnableShooting(false);
        }
    }

}
