using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        // Safely obtaining the PlayerController component
        playerController = GetComponentInParent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerGroundCheck: No PlayerController found in parent!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check to avoid self-collision and ensure the collider is not a trigger
        if (other.gameObject == playerController.gameObject || other.isTrigger)
            return;

        playerController.SetGroundedState(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject || other.isTrigger)
            return;

        playerController.SetGroundedState(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject || other.isTrigger)
            return;

        playerController.SetGroundedState(true);
    }
}
