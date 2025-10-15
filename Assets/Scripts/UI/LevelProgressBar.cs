using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;  
    [SerializeField] private Slider progressSlider; 

    [Header("Position Settings")]
    [SerializeField] private float startPointZ = 0f;  
    [SerializeField] private float endPointZ = 80f;  

    private void Update()
    {
        float currentZ = player.position.z;
        float progress = Mathf.InverseLerp(startPointZ, endPointZ, currentZ);
        progressSlider.value = progress;//karakterin kat ettiði mesafe
    }
}
