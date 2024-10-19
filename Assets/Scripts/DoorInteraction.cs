using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player;      // Reference to the player
    public GameObject enterLabel;  // Reference to the label UI (Text object)
    public float detectionRange = 3f; // Range at which the player can interact
    private bool isNearDoor = false;
    [SerializeField]
    private StarterAssetsInputs _input;
    [SerializeField]
    private GameObject teleportPoint;
    [SerializeField]
    private String textInteractive;
    private CharacterController controller;
    void Start()
    {
        //_input = transform.root.GetComponent<StarterAssetsInputs>();
        enterLabel.SetActive(false); // Hide the label initially
        controller = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the interaction range
        if (distance <= detectionRange)
        {

            enterLabel.SetActive(true);  // Show the label
            enterLabel.GetComponentInChildren<TextMeshProUGUI>().text = textInteractive;
            isNearDoor = true;
        }
        else if (distance <= detectionRange + 1 && distance > detectionRange)
        {
            enterLabel.SetActive(false); // Hide the label
            isNearDoor = false;
        }
        
        // If the player is near the door and presses "E"
        if (isNearDoor && _input.interactive)
        {
            InteractWithDoor(); // Call your custom action
        }
    }

     // Teleport the player to the target position
    void InteractWithDoor()
    {
        // Disable CharacterController to avoid issues
        controller.enabled = false;

        Debug.Log(teleportPoint.transform.position);

        // Teleport the player to the target position
        player.transform.position = teleportPoint.transform.position;

        // Re-enable CharacterController after teleportation
        controller.enabled = true;

        Debug.Log("Player teleported!");

        //_input.interactive = false;

        isNearDoor = false;
    }
}
