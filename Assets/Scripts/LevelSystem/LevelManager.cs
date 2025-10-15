using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ILevelLoader
{
    GameObject LoadLevel(int levelIndex, Transform parent = null);
}

public class LevelManager : MonoBehaviour
{
    [Header("Level List")]
    [SerializeField] private LevelData[] levels;

    [Header("Pool Reference")]
    [SerializeField] private LevelPool levelPool;

    [Header("UI & Player")]
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private Vector3 playerStartPosition = new Vector3(0, 0.7f, -6);

    public GameObject NextLevelPanel => nextLevelPanel;
    private DynamicObstacleManager dynamicManager;

    private int currentLevelIndex = 0;

    private void Start()
    {
        if (levels == null || levels.Length == 0)
        {
           // Debug.LogError("level atanmadi");
            return;
        }

        if (levelPool == null)
        {
           // Debug.LogError("level pool atanmadı");
            return;
        }

        dynamicManager = FindObjectOfType<DynamicObstacleManager>();
        LoadLevel(currentLevelIndex);

        if (nextLevelPanel != null)
            nextLevelPanel.SetActive(false);
    }

    public int GetCurrentLevelIndex() => currentLevelIndex;
    public int GetTotalLevelCount() => levels.Length;

    public void LoadLevel(int index)
    {
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);

        StopAllCoroutines();

        if (index < 0 || index >= levels.Length)
            return;

        currentLevelIndex = index;
        LevelData data = levels[currentLevelIndex];

        levelPool.ResetPool();

        // engeller yerlestirme ve llevel pool olusumu
        if (dynamicManager != null)
            dynamicManager.InitializeDynamicLevel(data, levelPool);

        RespawnPlayer();
        Time.timeScale = 1f;
    }
    public void CompleteLevel()
    {
        var saveSystem = FindObjectOfType<LevelSaveSystem>();
        saveSystem?.SaveProgress();

        if (currentLevelIndex >= levels.Length - 1)
        {
            saveSystem?.ShowCongratulationsPanel();
        }
        else
        {
            saveSystem?.ShowNextLevelPanel();
        }
    }

    public void NextLevel()
    {
        CompleteLevel();
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
           // Debug.LogWarning("player bulunammadi");
            return;
        }

        // oyuncunun baslangic pos
        player.transform.position = playerStartPosition;
        player.transform.rotation = Quaternion.identity;


        BallThrower thrower = FindObjectOfType<BallThrower>();
        if (thrower != null)
        {
            if (thrower.shootPoint == null)
            {
                Transform shoot = player.transform.Find("ShootPoint");
                if (shoot != null)
                    thrower.shootPoint = shoot;
            }

            thrower.enabled = false;
            thrower.enabled = true;
        }
    }

    public void RestartFromFirstLevel()
    {
        currentLevelIndex = 0;
        LoadLevel(currentLevelIndex);
    }

    public void SetCurrentLevel(int index)
    {
        currentLevelIndex = index;
    }

    public void LoadNextLevelDirect()
    {
        // Son level degilse bir sonrakini yuker
        if (currentLevelIndex < levels.Length - 1)
        {
            currentLevelIndex++;
           // Debug.Log("Level {currentLevelIndex} yüklendi ");
            LoadLevel(currentLevelIndex);
        }
        else
        {
          //  Debug.Log("levveler tamamlandi");
            var saveSystem = FindObjectOfType<LevelSaveSystem>();
            saveSystem?.ShowCongratulationsPanel();
        }
    }
}
