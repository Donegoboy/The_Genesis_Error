using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public float jumpDistanceMultiplier = 2f; 
    private float tileSize = 1f; 

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