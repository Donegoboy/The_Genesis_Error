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
    public AudioClip teleportSfx;

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
            if (teleportSfx != null)
            {
                GameObject soundObject = new GameObject("CoinSFX");
                soundObject.transform.position = transform.position;

                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                audioSource.clip = teleportSfx;
                audioSource.spatialBlend = 0f;
                audioSource.Play();
                audioSource.pitch = 2.4f;
                Destroy(soundObject, audioSource.clip.length);
            }

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
