using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private UnityEngine.UI.Image[] heartImages; 
    [SerializeField] private GameObject replayPanel;
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("DOTween Settings")]
    [SerializeField] private float destroyDuration = 0.3f;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();

        if (replayPanel != null)
            replayPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
    private void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHeartsUI();

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (replayPanel != null)
            replayPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void UpdateHeartsUI() //ikonları guncelle
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] == null) continue;

            bool isActive = i < currentHealth;

            if (isActive)
            {
                heartImages[i].enabled = true;
                heartImages[i].transform.localScale = Vector3.one;
            }
            else
            {
                heartImages[i].transform.DOKill();
                heartImages[i].transform.DOScale(0, destroyDuration).OnComplete(() =>
                {
                    heartImages[i].enabled = false;
                    heartImages[i].transform.localScale = Vector3.one;
                });
            }
        }
    }
    public void FullHealth()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] == null) continue;

            heartImages[i].transform.DOKill();
            heartImages[i].enabled = i < currentHealth;
            heartImages[i].transform.localScale = Vector3.one;
        }
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }
}
