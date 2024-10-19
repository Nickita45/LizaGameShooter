using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;  // Set enemy's initial health

    private float maxHealth;
    [SerializeField]
    private Image _healthImageBar;
    public void Start()
    {
        this.maxHealth = health;
        _healthImageBar.fillAmount = health/maxHealth;
    }    

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;  // Reduce health by the damage amount
        _healthImageBar.fillAmount = health/maxHealth;
        // Check if health is below or equal to 0
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add any death effects or logic here, like playing a death animation
        Debug.Log(gameObject.name + " has died!");

        // Destroy the enemy object
        Destroy(gameObject);
    }
}
