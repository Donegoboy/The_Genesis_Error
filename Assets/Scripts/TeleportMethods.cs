using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMethods : MonoBehaviour        //Znam da moze biti bolje... :(
{
    public static IEnumerator PerformBasicTeleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 1, 0) * tileSize; // North
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // South
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // West
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize; // East
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition;

        // Correctly snap the player's Y position to the center of the tile
        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }
    public static IEnumerator PerformType2Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // North -> West
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // South -> South
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // West -> South
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // East -> South
            facingDirection = Vector2.down;
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Update player position

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection);

        float targetAngle = 0;
        switch (facingDirection.x, facingDirection.y)
        {
            case (0, 1): // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0): // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType3Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, +1, 0) * tileSize; // North -> North
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, +1, 0) * tileSize; // South -> North
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // West -> South
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // East -> South
            facingDirection = Vector2.down;
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition;

        // Correctly snap the player's Y position to the center of the tile
        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection);
        float targetAngle = 0f;
        switch (facingDirection.x, facingDirection.y)
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType4Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player object
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // North -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // South -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // West -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // East -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player object

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player object
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player object
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection); // Use the updated facingDirection
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player object
        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType5Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player object
        Vector3 offset = Vector3.zero;

        // Since this is for left/right movement only, we only care about those directions
        if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // West
            facingDirection = Vector2.left; // Face West after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize; // East
            facingDirection = Vector2.right; // Face East after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player object

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player object
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player object
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection);  // Update facing direction
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player object
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player object

        // Remove the following line (it's likely not needed):
        // player.transform.rotation = player.GetTargetRotation();

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType6Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 1, 0) * tileSize; // North
            facingDirection = Vector2.up; // Face North after teleporting
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // West
            facingDirection = Vector2.left; // Face West after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize; // East
            facingDirection = Vector2.right; // Face East after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection); // Update facing direction
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player

        // Remove the following line (it's likely not needed):
        // transform.rotation = player.targetRotation;

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType7Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // North -> West
            facingDirection = Vector2.left; // Face West after teleporting
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; // South -> West
            facingDirection = Vector2.left; // Face West after teleporting
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // West -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, 1, 0) * tileSize; // East -> North
            facingDirection = Vector2.up; // Face North after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection); // Update facing direction
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player

        // Remove the following line (it's likely not needed):
        // transform.rotation = player.targetRotation;

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType8Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 0, 0) * tileSize; // North -> No movement
            facingDirection = Vector2.up; // Remains facing North
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(1, 0, 0) * tileSize; // South -> East
            facingDirection = Vector2.right; // Face East after teleporting
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, 1, 0) * tileSize; // West -> North
            facingDirection = Vector2.up; // Face North after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // East -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection); // Update facing direction
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player

        // Remove the following line (it's likely not needed):
        // transform.rotation = player.targetRotation;

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }

    public static IEnumerator PerformType9Teleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        // Get the destination transform from the Teleporter script
        Transform destinationTransform = teleporter.GetDestination();

        // Calculate offset based on facing direction
        Vector2 facingDirection = player.GetFacingDirection(); // Access through player
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(1, 0, 0) * tileSize; // North -> East
            facingDirection = Vector2.right; // Face East after teleporting
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, 1, 0) * tileSize; // South -> North
            facingDirection = Vector2.up; // Face North after teleporting
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // West -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize; // East -> South
            facingDirection = Vector2.down; // Face South after teleporting
        }

        // Calculate the new position with offset
        Vector3 newPosition = destinationTransform.position + offset;

        // Wait for a short duration to prevent re-triggering the teleporter
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        // Teleport the player to the new position
        player.transform.position = newPosition; // Access through player

        // Snap the player's position to the center of the tile
        player.transform.position = new Vector3( // Access through player
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3( // Access through player
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        // Rotate the player to face the new direction
        player.SetFacingDirection(facingDirection); // Update facing direction
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y) // Access through player
        {
            case (0, 1):  // Up
                targetAngle = 0;
                break;
            case (0, -1): // Down
                targetAngle = 180;
                break;
            case (-1, 0): // Left
                targetAngle = -90;
                break;
            case (1, 0):  // Right
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); // Access through player

        // Remove the following line (it's likely not needed):
        // transform.rotation = player.targetRotation;

        // Check for holes at the new position
        player.CheckForHole();

        // Reset flags after teleportation
        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }
}