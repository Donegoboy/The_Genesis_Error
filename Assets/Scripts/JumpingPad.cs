using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public float jumpDistanceMultiplier = 2f; // Multiplier for the jump distance
    private float tileSize = 1f; // Set your tile size here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Snap player to the jumping pad using the provided logic
                SnapPlayerToPad(playerMovement);

                // Stop any ongoing continuous movement
                playerMovement.SetMoving(false);

                playerMovement.SetCanJump(true);
                Debug.Log("Launching player!");
                playerMovement.LaunchPlayer(jumpDistanceMultiplier);


            }
        }
    }

    private void SnapPlayerToPad(PlayerMovement playerMovement)
    {
        // Get the collider of the jumping pad
        BoxCollider2D padCollider = GetComponent<BoxCollider2D>();

        if (padCollider != null)
        {
            // Calculate the center position of the jumping pad
            Vector3 padCenter = padCollider.bounds.center;

            // Snap the player's position to the center of the jumping pad using your logic
            playerMovement.transform.position = new Vector3(
                Mathf.Round((padCenter.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                Mathf.Round((padCenter.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                playerMovement.transform.position.z
            );
        }
    }
}