using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public float teleportCooldown = 0.2f;
    private bool canTeleport = true;

    private TeleportManager teleportManager;

    private void Start()
    {
        teleportManager = FindObjectOfType<TeleportManager>();
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
