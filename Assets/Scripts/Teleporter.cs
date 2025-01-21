using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public float teleportCooldown = 0.2f; // Cooldown time
    private bool canTeleport = true;

    // Reference to the TeleportManager
    private TeleportManager teleportManager;

    private void Start()
    {
        // Find the TeleportManager in the scene
        teleportManager = FindObjectOfType<TeleportManager>();
        if (teleportManager == null)
        {
            Debug.LogError("TeleportManager not found in the scene!");
        }
    }

    public Transform GetDestination()
    {
        return destination;
    }

    public void TeleportPlayer(PlayerMovement player)
    {
        if (canTeleport)
        {
            StartCoroutine(Cooldown());
            // Use TeleportManager to handle the teleport logic
            teleportManager.HandleTeleport(player, this);
        }
    }

    private IEnumerator Cooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
