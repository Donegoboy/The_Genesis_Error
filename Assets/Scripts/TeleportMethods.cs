using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMethods : MonoBehaviour        //Znam da je koma, ali pokusao sam smanjiti redudantnost i nije mi islo...
{
    public static IEnumerator PerformBasicTeleport(PlayerMovement player, Teleporter teleporter, float tileSize)
    {
        player.SetTeleporting(true);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(false);

        Transform destinationTransform = teleporter.GetDestination();

        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 1, 0) * tileSize;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize; 
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; 
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize; 
        }

        Vector3 newPosition = destinationTransform.position + offset;

        yield return new WaitForSeconds(teleporter.teleportCooldown);

        player.transform.position = newPosition;

        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.CheckForHole();
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

        Transform destinationTransform = teleporter.GetDestination();

        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3((float)-0.8, 0, 0) * tileSize;
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }

        Vector3 newPosition = destinationTransform.position + offset;

        yield return new WaitForSeconds(teleporter.teleportCooldown);

        player.transform.position = newPosition; 

        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);

        float targetAngle = 0;
        switch (facingDirection.x, facingDirection.y)
        {
            case (0, 1): 
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0): 
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();
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

        Transform destinationTransform = teleporter.GetDestination();

        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;
        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, +1, 0) * tileSize; 
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, +1, 0) * tileSize; 
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize; 
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);

        player.transform.position = newPosition;
        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0f;
        switch (facingDirection.x, facingDirection.y)
        {
            case (0, 1): 
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0): 
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));


        player.CheckForHole();
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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection(); 
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down; 
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition;

        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();

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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection(); 
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize;
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize;
            facingDirection = Vector2.right;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition;

        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();

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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 1, 0) * tileSize;
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(-1, 0, 0) * tileSize;
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(1, 0, 0) * tileSize;
            facingDirection = Vector2.right;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition;

        player.transform.position = new Vector3( 
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();

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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(-1, 0, 0) * tileSize;
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(-1, 0, 0) * tileSize; 
            facingDirection = Vector2.left;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, 1, 0) * tileSize; 
            facingDirection = Vector2.up;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition; 

        player.transform.position = new Vector3( 
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();

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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(0, 0, 0) * tileSize;
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(1, 0, 0) * tileSize;
            facingDirection = Vector2.right;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, 1, 0) * tileSize;
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition;

 
        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1):
                targetAngle = 180;
                break;
            case (-1, 0):
                targetAngle = -90;
                break;
            case (1, 0):
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle));
        player.CheckForHole();

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

        Transform destinationTransform = teleporter.GetDestination();
        Vector2 facingDirection = player.GetFacingDirection();
        Vector3 offset = Vector3.zero;

        if (facingDirection == Vector2.up)
        {
            offset = new Vector3(1, 0, 0) * tileSize; 
            facingDirection = Vector2.right;
        }
        else if (facingDirection == Vector2.down)
        {
            offset = new Vector3(0, 1, 0) * tileSize;
            facingDirection = Vector2.up;
        }
        else if (facingDirection == Vector2.left)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }
        else if (facingDirection == Vector2.right)
        {
            offset = new Vector3(0, -1, 0) * tileSize;
            facingDirection = Vector2.down;
        }

        Vector3 newPosition = destinationTransform.position + offset;
        yield return new WaitForSeconds(teleporter.teleportCooldown);
        player.transform.position = newPosition;

        player.transform.position = new Vector3(
            player.transform.position.x,
            Mathf.Round((player.transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.z
        );

        player.transform.position = new Vector3(
            Mathf.Round((player.transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
            player.transform.position.y,
            player.transform.position.z
        );

        player.SetFacingDirection(facingDirection);
        float targetAngle = 0;
        switch (player.GetFacingDirection().x, player.GetFacingDirection().y)
        {
            case (0, 1):
                targetAngle = 0;
                break;
            case (0, -1): 
                targetAngle = 180;
                break;
            case (-1, 0): 
                targetAngle = -90;
                break;
            case (1, 0): 
                targetAngle = 90;
                break;
        }
        player.SetTargetRotation(Quaternion.Euler(0, 0, targetAngle)); 
        player.CheckForHole();

        player.SetTeleporting(false);
        player.SetMoving(false);
        player.SetJumping(false);
        player.SetCanJump(true);
    }
}