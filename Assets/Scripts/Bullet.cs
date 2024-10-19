using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 20f;  // Set how much damage the bullet does

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object it collided with has an EnemyHealth component
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            // Apply damage to the enemy
            enemy.TakeDamage(damage);

            // Destroy the bullet after it hits an enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet if it hits something else after a delay
            Destroy(gameObject, 2f);
        }
    }
}
