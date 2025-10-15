using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void SetupSlider(float maxValue)
    {
        if (slider != null)
            slider.maxValue = maxValue;
    }

    public void UpdateHealthBar(float currentValue, float maxValue, int remainingHealth = -1)
    {
        if (slider != null)
            slider.value = currentValue;  
    }
  /*  public void UpdateHealthBar(float currentValue, float maxValue, int remainingHealth = -1)
    {
        if (slider != null)
            slider.value = currentValue / maxValue;
    }*/
}
