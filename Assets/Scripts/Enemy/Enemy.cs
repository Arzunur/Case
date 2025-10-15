using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamageable
{
    void TakeDamage(float amount);
}
public class Enemy : MonoBehaviour,IDamageable
{
    [Header("Enemy Stats")]
    [SerializeField] private int durability = 5;         
    [SerializeField] private float damage = 20f; 

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar")]
    [SerializeField] private EnemyHealthBar enemyHealthBar;

    private void Awake()
    {
        currentHealth = maxHealth; 
        if (enemyHealthBar == null)
            enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        if (enemyHealthBar != null)
            enemyHealthBar.SetupSlider(maxHealth);

        UpdateHealthUI();

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateHealthUI()
    {
        if(enemyHealthBar != null)
    {
            enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
    private void Die()
    {
        //Animasyon eklenecek
        Destroy(gameObject);
    }
}
