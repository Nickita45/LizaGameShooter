using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [Header("Melee Attack Settings")]
    [SerializeField] private float attackRange = 2f;  // Range of melee attack
    [SerializeField] private int attackDamage = 20;   // Damage dealt by the sword
    [SerializeField] private Transform swordPoint;    // The point from where we will detect hits (e.g., end of the sword)
    [SerializeField] private LayerMask enemyLayer;    // Layer that defines enemies

    [Header("Animation")]
    private Animation swordAnimation;

    private bool isAttacking = false;  // Flag to prevent multiple attacks at once

    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        swordAnimation = GetComponent<Animation>(); // Assuming the sword has an animation component
    }

    // Update is called once per frame
    void Update()
    {
        // Melee Attack logic
        if (_input.shoot && !isAttacking)
        {
            MeleeAttack();
            _input.shoot = false; // Reset melee input
        }
    }

    void MeleeAttack()
    {
        // Play sword attack animation
        if (!swordAnimation.isPlaying)
        {
            StartCoroutine(PerformMeleeAttack());
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;

        // Play the sword attack animation
        swordAnimation.Play();

        // Wait for the attack animation to reach the hit detection moment
        yield return new WaitForSeconds(0.3f); // Adjust based on your animation timing

        // Perform hit detection during the swing


        // Wait for the attack animation to finish
        yield return new WaitForSeconds(swordAnimation["SwordAnim"].length);

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Check if the sword collides with an enemy (assuming they have the "Enemy" tag)
        if (other.CompareTag("Enemy") && isAttacking)
        {

            // Get the EnemyHealth component from the enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
}
