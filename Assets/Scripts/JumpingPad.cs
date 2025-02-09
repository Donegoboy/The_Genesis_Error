using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public float jumpDistanceMultiplier = 2f; 
    private float tileSize = 1f;
    public AudioClip jumpingPadSfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {            
                SnapPlayerToPad(playerMovement);
                playerMovement.SetMoving(false);
                playerMovement.SetCanJump(true);

                if (jumpingPadSfx != null)
                {
                    GameObject soundObject = new GameObject("jumpingPadSfx");
                    soundObject.transform.position = transform.position;

                    AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                    audioSource.clip = jumpingPadSfx;
                    audioSource.spatialBlend = 0f;
                    //audioSource.pitch = 2.3f;
                    audioSource.Play();
                    Destroy(soundObject, audioSource.clip.length);
                }

                playerMovement.LaunchPlayer(jumpDistanceMultiplier);
            }
        }
    }

    private void SnapPlayerToPad(PlayerMovement playerMovement)
    {
        BoxCollider2D padCollider = GetComponent<BoxCollider2D>();

        if (padCollider != null)
        {
            Vector3 padCenter = padCollider.bounds.center;

            playerMovement.transform.position = new Vector3(
                Mathf.Round((padCenter.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                Mathf.Round((padCenter.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                playerMovement.transform.position.z
            );
        }
    }
}