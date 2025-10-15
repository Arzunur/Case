using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (levelManager != null)
                levelManager.CompleteLevel(); // Panel acilir ve kaydedilir
            else
                Debug.LogWarning("LevelManager referansý atanmadý!");
        }
    }
}
