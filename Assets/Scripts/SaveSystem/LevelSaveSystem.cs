using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelSaveSystem : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI startButtonText;
    [SerializeField] private GameObject congratulationsPanel;

    private const string SAVE_KEY = "SavedLevelIndex";

    private void Start()
    {
        if (continuePanel != null)
            continuePanel.SetActive(false);
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);

        LoadProgress();
    }

    //level kaydetme
    public void SaveProgress()
    {
        if (levelManager == null) return;

        int current = levelManager.GetCurrentLevelIndex();
        PlayerPrefs.SetInt(SAVE_KEY, current);
        PlayerPrefs.Save();

        UpdateContinuePanelText();
        UpdateStartButtonText();
    }
    //level yukleme
    public void LoadProgress()
    {
        int savedIndex = PlayerPrefs.GetInt(SAVE_KEY, 0);

        if (levelManager != null)
        {
            levelManager.SetCurrentLevel(savedIndex);
            levelManager.LoadLevel(savedIndex);
        }

        //Debug.Log($" Level yüklendi: {savedIndex}");
        UpdateContinuePanelText();
        UpdateStartButtonText();
    }

    public void ShowNextLevelPanel()
    {
        if (continuePanel != null)
        {
            continuePanel.SetActive(true);
            Time.timeScale = 0f; 
        }

        UpdateContinuePanelText();
    }

    public void ContinueGameAndNextLevel()
    {
      //  Debug.Log("Continue tıklandı ");

        Time.timeScale = 1f;

        if (continuePanel != null)
            continuePanel.SetActive(false);

        if (levelManager != null)
        {
            levelManager.LoadNextLevelDirect();
        }

        if (levelManager != null)
        {
            int current = levelManager.GetCurrentLevelIndex();
            PlayerPrefs.SetInt("SavedLevelIndex", current);
            PlayerPrefs.Save();
        }
    }

    private void UpdateContinuePanelText()
    {
        if (levelManager == null || nextLevelText == null) return;

        int currentLevel = levelManager.GetCurrentLevelIndex();
        int totalLevels = levelManager.GetTotalLevelCount();

        nextLevelText.text = (currentLevel + 1 >= totalLevels)
            ? "All levels completed!"
            : $"Next Level: {currentLevel + 2}";
    }

    public void UpdateStartButtonText()
    {
        if (startButtonText == null || levelManager == null) return;

        int savedIndex = levelManager.GetCurrentLevelIndex();
        startButtonText.text = $"Start Level {savedIndex + 1}";
    }

    public void ShowCongratulationsPanel()
    {
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(true);
    }

    public void StartFromLevelOne()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);

        if (levelManager != null)
        {
            levelManager.SetCurrentLevel(0);
            levelManager.LoadLevel(0);
        }

        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);
        if (continuePanel != null)
            continuePanel.SetActive(false);

        UpdateStartButtonText();
        UpdateContinuePanelText();

        Time.timeScale = 1f;
    }
}
