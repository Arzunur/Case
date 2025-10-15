using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ButtonTextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText; 
    [SerializeField] private float duration = 0.5f; 
    [SerializeField] private float scaleAmount = 1.2f; 

    private void Start()
    {
        AnimText();
    }

    public void AnimText()
    {
     //  buttonText.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        buttonText.transform
            .DOScale(scaleAmount, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
