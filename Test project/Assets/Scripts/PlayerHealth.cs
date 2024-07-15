using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject deadPanel;

    void Start()
    {
        currentHealth = maxHealth;
        deadPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar healthBar = gameObject.GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Player takes damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died.");
        // Здесь можно написать логику, которая происходит при смерти игрока
        deadPanel.SetActive(true);
    }
}
