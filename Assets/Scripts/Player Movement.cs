using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float tileSize = 1f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpDistance = 2f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask surfaceLayer; // Use for movement collision checks
    [SerializeField] private LayerMask holeLayer; // Use for hole detection
    [SerializeField] private LayerMask teleporterLayer;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    public bool isJumping = false;
    private Vector3 targetPosition;
    public Vector2 facingDirection = Vector2.up;
    public bool isMoving = false;
    public bool canJump = true; // Flag to allow jumping only when stationary
    private bool isRotating = false;
    public Quaternion targetRotation;
    private bool isDestroyed = false; // Flag to indicate if the player is destroyed
    private Coroutine snapCoroutine;

    private GameObject activeCamera;

    public bool isTeleporting = false;

    public void SetTeleporting(bool value) { isTeleporting = value; }
    public void SetMoving(bool value) { isMoving = value; }
    public void SetJumping(bool value) { isJumping = value; }

    public void SetCanJump(bool value) { canJump = value; }
    public Vector2 GetFacingDirection() { return facingDirection; }
    public void SetFacingDirection(Vector2 newDirection) { facingDirection = newDirection; }

    public void SetTargetRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // Find and store reference to the initially active camera
        if (Camera.main != null)
        {
            activeCamera = Camera.main.gameObject;
        }
    }

    private void Update()
    {
        if (!isDestroyed && !isTeleporting)
        {     
            HandleInput();     
            Move();
            RotatePlayer();
        }
    }

    private void HandleInput()
    {
        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isRotating && !isMoving)
        {
            AttemptJump();
            return;
        }
        // Restrict movement input to one direction at a time
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            AttemptRotate(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            AttemptRotate(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            AttemptRotate(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            AttemptRotate(Vector2.right);
        }

        if (!isMoving && !isRotating)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                AttemptMove(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                AttemptMove(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                AttemptMove(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                AttemptMove(Vector2.right);
            }
        }
    }
    private void AttemptRotate(Vector2 direction)
    {
        if (!isRotating && direction != facingDirection)
        {
            facingDirection = direction;
            isRotating = true;
            canJump = false; // Disallow jumping while rotating

            // Calculate target rotation based on direction
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Rotate the active camera
            StartCoroutine(RotateCameraCoroutine(direction));
        }
    }

    private IEnumerator RotateCameraCoroutine(Vector2 direction)
    {
        if (activeCamera == null) yield break;

        Quaternion startRotation = activeCamera.transform.rotation;
        Quaternion cameraTargetRotation = Quaternion.Euler(0, 0, targetRotation.eulerAngles.z); // Use the same angle as player

        float startTime = Time.time;
        float rotationTime = 0f;

        while (rotationTime < 1f)
        {
            rotationTime = (Time.time - startTime) * rotationSpeed / 90f; // Normalize by 90 degrees
            activeCamera.transform.rotation = Quaternion.Slerp(startRotation, cameraTargetRotation, rotationTime);
            yield return null;
        }

        activeCamera.transform.rotation = cameraTargetRotation; // Ensure it ends at the exact target rotation
    }

    private void RotatePlayer()
    {
        if (isRotating)
        {
            // Rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if rotation is complete (within a small tolerance)
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; // Snap to exact rotation
                isRotating = false;
                canJump = true; // Allow jumping again after rotation
            }
        }
    }
    private void AttemptMove(Vector2 direction)
    {
        // Use facingDirection for movement calculation
        Vector3 proposedPosition = transform.position + new Vector3(facingDirection.x, facingDirection.y, 0) * tileSize;

        // Check for obstacles on the surface layer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, tileSize, surfaceLayer);

        if (hit.collider != null)
        {
            // There is something on the surface layer
            if (hit.collider.CompareTag("Teleport") || hit.collider.CompareTag("TeleportType2") || hit.collider.CompareTag("TeleportType3") ||
                hit.collider.CompareTag("TeleportType4") || hit.collider.CompareTag("TeleportType5") || hit.collider.CompareTag("TeleportType6") ||
                hit.collider.CompareTag("TeleportType7") || hit.collider.CompareTag("TeleportType8") || hit.collider.CompareTag("TeleportType9"))
            {
                // The object is a teleporter, allow movement 
                targetPosition = proposedPosition;
                isMoving = true;
                canJump = false;
                return; // Skip further checks
            }
            else
            {
                // Obstacle on the surface layer is not a teleporter
                isMoving = false;
                canJump = true;
                return; // Don't move
            }
        }

        // No obstacle on the surface layer, check for hole
        if (Physics2D.OverlapCircle(proposedPosition, circleCollider.radius * 0.7f, holeLayer))
        {
            isMoving = false;
            canJump = true;
            return; // Don't move to the hole
        }

        // No obstacles, update target position and start moving
        targetPosition = proposedPosition;
        isMoving = true;
        canJump = false; // Disallow jumping while moving
    }

    public void LaunchPlayer(float jumpDistanceMultiplier)
    {
        Debug.Log("LaunchPlayer called! Multiplier: " + jumpDistanceMultiplier);
        // Force these values to allow the jump
        canJump = true;
        isMoving = false;

        if (canJump && !isRotating && !isMoving)
        {
            AttemptJump(jumpDistanceMultiplier);
        }
    }

    private void AttemptJump(float jumpDistanceMultiplier = 1f)
    {
        // Use facingDirection for jump calculation
        Vector3 proposedPosition = transform.position + new Vector3(facingDirection.x, facingDirection.y, 0) * tileSize * jumpDistance * jumpDistanceMultiplier;

        // Check for OneWayTile (using a separate raycast)
        RaycastHit2D oneWayHit = Physics2D.Raycast(transform.position, facingDirection, tileSize * jumpDistance * jumpDistanceMultiplier, surfaceLayer);

        if (oneWayHit.collider != null && oneWayHit.collider.CompareTag("OneWayTile"))
        {
            // Blocked by OneWayTile - do nothing (or add feedback)
            Debug.Log("Jump blocked by OneWayTile");
            canJump = false; // Prevent jumping
            isJumping = false;
        }
        else
        {
            // Not blocked, proceed with jump
            targetPosition = proposedPosition;
            isJumping = true;
            canJump = false; // Disallow jumping while jumping
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        // Teleport to the target position
        transform.position = targetPosition;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        isJumping = false;
        canJump = true; // Allow jumping again after teleporting

        CheckForHole(); // Check for hole after jumping

        yield return null;
    }

    private void Move()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the player has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition; // Snap to position

                // Check if there's an obstacle in the current facing direction
                RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, tileSize, surfaceLayer);
                if (hit.collider != null)
                {
                    isMoving = false; // Stop moving if there's an obstacle
                    canJump = true; // Allow jumping again after movement stops
                }
                else
                {
                    // No obstacle, continue moving in the same direction if the key is still held down
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && facingDirection == Vector2.up)
                    {
                        AttemptMove(facingDirection);
                    }
                    else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && facingDirection == Vector2.down)
                    {
                        AttemptMove(facingDirection);
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && facingDirection == Vector2.left)
                    {
                        AttemptMove(facingDirection);
                    }
                    else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && facingDirection == Vector2.right)
                    {
                        AttemptMove(facingDirection);
                    }
                    else
                    {
                        isMoving = false;
                        canJump = true;
                    }
                }
            }

            // Check for holes
            CheckForHole();
        }
        else
        {
            canJump = true; // Ensure jumping is allowed when not moving
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting) return; // Ignore trigger if already teleporting

        if (other.CompareTag("Teleport") || other.CompareTag("TeleportType2") || other.CompareTag("TeleportType3") ||
                other.CompareTag("TeleportType4") || other.CompareTag("TeleportType5") || other.CompareTag("TeleportType6") ||
                other.CompareTag("TeleportType7") || other.CompareTag("TeleportType8") || other.CompareTag("TeleportType9"))
        {
            Teleporter teleporter = other.GetComponent<Teleporter>();
            if (teleporter != null)
            {
                teleporter.TeleportPlayer(this);
            }
        }
        if (other.CompareTag("EdgeTopA")) // Replace "YourTilemapTag" with the actual tag of your tilemap
        {
            Tilemap tilemap = other.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Debug.Log("Smetlje");
                ChangeTilemapOrderInLayer(tilemap, 2); // Set a higher order in layer when player enters
            }
        }
        if (isTeleporting) return;
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EdgeTopA")) // Replace "YourTilemapTag" with the actual tag of your tilemap
        {
            Tilemap tilemap = other.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                ChangeTilemapOrderInLayer(tilemap, 0); // Reset order in layer when player exits
            }
        }
    }

    private void ChangeTilemapOrderInLayer(Tilemap tilemap, int orderInLayer)
    {
        TilemapRenderer renderer = tilemap.GetComponent<TilemapRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = orderInLayer;
        }
    }

    public void CheckForHole()
    {
        // Check if the player is over a hole tile only if they are not jumping
        if (!isJumping)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, circleCollider.radius * 0.7f, holeLayer);
            if (hit != null)
            {
                Debug.Log("Player has fallen into a hole!");
                // Disable further input and movement
                isMoving = false;
                isRotating = false;
                canJump = false;
                isDestroyed = true;
                // Disable the collider to prevent further triggering
                circleCollider.enabled = false;
                Destroy(gameObject); // Or your game over logic
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (circleCollider == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleCollider.radius * 0.7f);
    }
    public void StopSnapCoroutine()
    {
        if (snapCoroutine != null)
        {
            StopCoroutine(snapCoroutine);
        }
    }
    public void SnapToGrid()
    {
        // Check if the GameObject is still active (not destroyed)
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            float tileSize = 1f; // Set your tile size here

            // Snap the player's position to the center of the nearest tile
            transform.position = new Vector3(
                Mathf.Round((transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                Mathf.Round((transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                transform.position.z
            );
        }
    }
}