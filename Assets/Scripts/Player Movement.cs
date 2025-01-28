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
    [SerializeField] private float jumpDuration = 0.5f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask surfaceLayer; 
    [SerializeField] private LayerMask holeLayer; 
    [SerializeField] private LayerMask teleporterLayer;
    [SerializeField] private LayerMask collisionLayers; 

    [SerializeField] private AnimationCurve playerJumpCurve;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider; 
    public bool isJumping = false;
    private Vector3 targetPosition;
    public Vector2 facingDirection = Vector2.up;
    public bool isMoving = false;
    public bool canJump = true; 
    private bool isRotating = false;
    public Quaternion targetRotation;
    private bool isDestroyed = false;
    private Coroutine snapCoroutine;
    private GameManager gameManager;

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
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>(); 
        rb.gravityScale = 0;
        rb.freezeRotation = true;

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
            canJump = false; 

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            targetRotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    private void RotatePlayer()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; 
                isRotating = false;
                canJump = true;
            }
        }
    }
    private void AttemptMove(Vector2 direction)
    {

        Vector3 proposedPosition = transform.position + new Vector3(facingDirection.x, facingDirection.y, 0) * tileSize;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, tileSize, surfaceLayer);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Teleport") || hit.collider.CompareTag("TeleportType2") || hit.collider.CompareTag("TeleportType3") ||
                hit.collider.CompareTag("TeleportType4") || hit.collider.CompareTag("TeleportType5") || hit.collider.CompareTag("TeleportType6") ||
                hit.collider.CompareTag("TeleportType7") || hit.collider.CompareTag("TeleportType8") || hit.collider.CompareTag("TeleportType9"))
            {
                targetPosition = proposedPosition;
                isMoving = true;
                canJump = false;
                return; 
            }
            else
            {
                isMoving = false;
                canJump = true;
                return; 
            }
        }
        if (Physics2D.OverlapCircle(proposedPosition, circleCollider.radius * 0.7f, holeLayer))
        {
            isMoving = false;
            canJump = true;
            return; 
        }
        targetPosition = proposedPosition;
        isMoving = true;
        canJump = false;
    }

    public void LaunchPlayer(float jumpDistanceMultiplier)
    {
        Debug.Log("LaunchPlayer called! Multiplier: " + jumpDistanceMultiplier);
        canJump = true;
        isMoving = false;

        if (canJump && !isRotating && !isMoving)
        {
            AttemptJump(jumpDistanceMultiplier);
        }
    }

    private void AttemptJump(float jumpDistanceMultiplier = 1f)
    {
        Vector3 proposedPosition = transform.position + new Vector3(facingDirection.x, facingDirection.y, 0) * tileSize * jumpDistance * jumpDistanceMultiplier;

        RaycastHit2D oneWayHit = Physics2D.Raycast(transform.position, facingDirection, tileSize * jumpDistance * jumpDistanceMultiplier, surfaceLayer);

        if (oneWayHit.collider != null && oneWayHit.collider.CompareTag("OneWayTile"))
        {       
            canJump = false; 
            isJumping = false;
        }
        else
        {
            targetPosition = proposedPosition;
            isJumping = true;
            canJump = false;
            StartCoroutine(LerpMovement());
        }
    }

    private IEnumerator LerpMovement()
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetPosition;

        isMoving = true; 
        canJump = false;
        isJumping = true;

        if (circleCollider != null)
        {
            circleCollider.enabled = false; 
        }

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float timer = Mathf.Clamp01(elapsed / jumpDuration); 

            float curve = playerJumpCurve.Evaluate(timer);

            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, curve);

            if (IsCollidingDuringLerp(newPosition))
            {
                transform.position = startPosition;
                isJumping = false;
                canJump = true;
                CheckForHole();
                yield break;
            }

            transform.position = newPosition;
            yield return null; 
        }

        if (circleCollider != null)
        {
            circleCollider.enabled = true;
        }

        transform.position = endPosition;
        isJumping = false;
        canJump = true;
        isMoving = false;
        CheckForHole();
    }

    private bool IsCollidingDuringLerp(Vector3 newPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(newPosition, circleCollider.radius, collisionLayers);

        foreach (Collider2D hit in hits)
        {
            if (hit != circleCollider)
            {
                if (hit.CompareTag("OneWayTile"))
                    continue;
                return true; 
            }
        }
        return false;
    }

    private void Move()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, tileSize, surfaceLayer);
                if (hit.collider != null)
                {
                    isMoving = false;
                    canJump = true;
                }
                else
                {
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

        }
        else
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting) return;

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
        if (other.CompareTag("Key"))
        {
            Key keyScript = other.GetComponent<Key>();
            keyScript.Collect();
        }
        if (isTeleporting) return;
    }

    public void CheckForHole()
    {
        if (!isJumping)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, circleCollider.radius * 0.7f, holeLayer);
            if (hit != null)
            {
                Debug.Log("Opal' si u jamu");
                isMoving = false;
                isRotating = false;
                canJump = false;
                isDestroyed = true;
                circleCollider.enabled = false;
                Destroy(gameObject);
                gameManager.onDeath();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (circleCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleCollider.radius * 0.7f);
    }

    public void SnapToGrid()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            float tileSize = 1f; 
            transform.position = new Vector3(
                Mathf.Round((transform.position.x - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                Mathf.Round((transform.position.y - tileSize / 2f) / tileSize) * tileSize + tileSize / 2f,
                transform.position.z
            );
        }
    }
}