using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevel : ILevelLoader
{
    private GameObject parentPrefab;
    private LevelData data;
    private LevelPool pool;
    public ProceduralLevel(GameObject parentPrefab, LevelData data, LevelPool pool)
    {
        this.parentPrefab = parentPrefab;
        this.data = data;
        this.pool = pool;
    }

    public GameObject LoadLevel(int levelIndex, Transform parent = null)
    {
        GameObject levelParent = Object.Instantiate(parentPrefab, parent);
        levelParent.name = "Level_" + levelIndex;

        float startZ = 0f; 

        for (int i = 0; i < data.obstacleCount; i++)
        {
            GameObject prefab = data.obstaclePrefabs[Random.Range(0, data.obstaclePrefabs.Length)];
            GameObject obstacle = pool.GetFromPool(prefab);

            // pool bos ise Instantiate edilecek
            if (obstacle == null)
            {
                obstacle = Object.Instantiate(prefab, levelParent.transform);
            }

            obstacle.transform.SetParent(levelParent.transform);

            // Pozisyonu sifirdan basliyacak
            obstacle.transform.localPosition = new Vector3(0, 0, 0); 

            obstacle.SetActive(true);
        }

        return levelParent;
    }
}

