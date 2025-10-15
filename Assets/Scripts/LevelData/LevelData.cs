using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]

public class LevelData : ScriptableObject
{
    [Header("Level Settings")]
    public GameObject[] obstaclePrefabs; 
    public int obstacleCount = 10;
    public Vector2 levelSize = new Vector2(20, 20);//levelin boyutu
    public float obstacleRadius = 1.5f;
}
