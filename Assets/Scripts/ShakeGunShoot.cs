using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ShakeGunShoot : MonoBehaviour
{
    [SerializeField] private CharacterController _controller; // Reference to the CharacterController
    private Vector3 initialGunLocalPosition; // To store the initial local position of the gun.
    private Vector3 lastPosition; // To store the player's last frame position for movement tracking.

     // Gun shake properties.
    [SerializeField] private float shakeAmount = 0.1f; // The intensity of the shake.
    [SerializeField] private float upwardShake = 0.1f; // Shake upward when jumping.
    [SerializeField] private float forwardShake = 0.05f; // Shake forward while running.
    [SerializeField] private float smoothFactor = 5f; // Smoothness of the shake.

    void Start()
    {
        //_controller = transform.root.GetComponent<CharacterController>(); // Get the CharacterController component.

        // Store the initial local position of the gun for shake effect.
        initialGunLocalPosition = transform.localPosition;  // LOCAL position as gun is child.

        // Initialize last position to current player position.
        lastPosition = transform.root.position;
    }

    void Update()
    {
        HandleGunShake(); // Call the function to handle gun shake based on movement.

    }


    // Handle the gun shake based on the player's movement state.
    void HandleGunShake()
    {
        Vector3 movementDirection = CalculateMovementDirection();
        bool isGrounded = _controller.isGrounded;

        // Handle Jumping (Player is moving upward while not grounded).
        if (!isGrounded && movementDirection.y > 0.1f)
        {
            //Debug.Log("Jump");
            ShakeGun(Vector3.up * upwardShake); // Move the gun up when jumping.
        }
        // Handle Falling (Player is moving downward while not grounded).
        else if (!isGrounded && movementDirection.y < -0.1f)
        {
            //Debug.Log("Fall");
            ShakeGun(Vector3.down * upwardShake); // Move the gun down when falling.
        }
        // Handle Running (Player is moving horizontally while grounded).
        else if (isGrounded && (movementDirection.x != 0 || movementDirection.z != 0))
        {
            //Debug.Log("Running");
            ShakeGun(Vector3.forward * forwardShake); // Move the gun forward when running.
        }
        // Idle State (Player is grounded and not moving).
        else if (isGrounded && movementDirection == Vector3.zero)
        {
            ResetGunPosition(); // Return the gun to its original position.
        }
    }

    // Method to calculate player's movement direction by comparing current and last position.
    Vector3 CalculateMovementDirection()
    {
        Vector3 currentPosition = transform.root.position;
        Vector3 movementDirection = currentPosition - lastPosition;
        lastPosition = currentPosition;

        // Return the movement direction, normalized.
        return movementDirection;
    }

    // Method to apply the shake effect by adjusting the gun's local position (as it's a child).
    void ShakeGun(Vector3 shakeDirection)
    {
        // Smoothly interpolate between the current local position and the shake target position.
        Vector3 targetPosition = initialGunLocalPosition + shakeDirection;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothFactor);
    }

    // Method to reset the gun to its original local position when the player is idle.
    void ResetGunPosition()
    {
        // Smoothly return the gun to its initial local position.
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialGunLocalPosition, Time.deltaTime * smoothFactor);
    }
}
