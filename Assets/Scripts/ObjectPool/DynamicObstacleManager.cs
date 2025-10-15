using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleManager : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField] private Transform cameraTransform;

    [Header("Spawn Settings")]
    [SerializeField] private float viewDistance = 40f;   // Kamera onu spawn mesafesi
    [SerializeField] private float despawnDistance = 20f; // Kamera arkasi despawn mesafesi
    [SerializeField] private float spacing = 5f;       

    private LevelData currentLevelData;
    private LevelPool levelPool;

    private List<(GameObject prefab, GameObject instance)> activeObstacles = new();

    private float lastSpawnZ = 0f;

    public void InitializeDynamicLevel(LevelData data, LevelPool pool)
    {
        // Eski engelleri temizleme kismi
        foreach (var (prefab, obstacle) in activeObstacles)
        {
            if (obstacle != null)
                pool.ReturnToPool(prefab, obstacle);
        }
        activeObstacles.Clear();

        currentLevelData = data;
        levelPool = pool;
        lastSpawnZ = cameraTransform.position.z;

        lastSpawnZ = 0f;

        for (int i = 0; i < 10; i++)
        {
            SpawnObstacle(i * spacing);
        }

    }

    private void Update()
    {
        if (currentLevelData == null || levelPool == null)
            return;

        while (lastSpawnZ < cameraTransform.position.z + viewDistance)
        {
            SpawnObstacle(lastSpawnZ + spacing);
        }

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            var (prefab, obstacle) = activeObstacles[i];
            if (obstacle == null) continue;

            if (obstacle.transform.position.z < cameraTransform.position.z - despawnDistance)
            {
                activeObstacles.RemoveAt(i);
                levelPool.ReturnToPool(prefab, obstacle);
            }
        }
    }

    //spawn obstacle
    private void SpawnObstacle(float zPos)
    {
        if (currentLevelData == null || currentLevelData.obstaclePrefabs == null || currentLevelData.obstaclePrefabs.Length == 0)
            return;

        GameObject prefab = currentLevelData.obstaclePrefabs[Random.Range(0, currentLevelData.obstaclePrefabs.Length)];
        GameObject obstacle = levelPool.GetFromPool(prefab) ?? Instantiate(prefab);

        float xPos = Random.Range(-currentLevelData.levelSize.x / 2, currentLevelData.levelSize.x / 2);
        obstacle.transform.position = new Vector3(xPos, 0f, zPos);
        obstacle.transform.rotation = Quaternion.identity;
        obstacle.SetActive(true);

        activeObstacles.Add((prefab, obstacle));
        lastSpawnZ = zPos;
    }

}
