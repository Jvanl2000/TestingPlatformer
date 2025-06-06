using UnityEngine;
using UnityEngine.InputSystem; // Make sure this is included

// Removed Unity.VisualScripting as it's not used in the provided code snippet

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private InputActionReference move;

    [Header("Player Stats")]
    [SerializeField] private float movementSpeed = 5f;

    private float moveDirection = 0f;
    public bool isOnMovingPlatform = false;
    public Rigidbody2D movingPlatformBody;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.action.Enable();

        move.action.performed += OnMovePerformed; // Subscribe to move performed
        move.action.canceled += OnMoveCanceled;   // Subscribe to move canceled
    }

    private void OnDisable()
    {
        move.action.Disable();

        move.action.performed -= OnMovePerformed;
        move.action.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float>(); // Should be 0 when canceled for a standard axis
    }


    private void Update()
    {
        if (transform.position.y <= -50)
        {
            transform.position = Vector3.zero;
            if (body != null) body.linearVelocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (body != null)
        {
            if (isOnMovingPlatform && movingPlatformBody != null)
            {
                Vector2 platformVelocity = movingPlatformBody.linearVelocity;
                body.linearVelocity = new Vector2(moveDirection * movementSpeed + platformVelocity.x, body.linearVelocity.y);
            }
            else
            {
                body.linearVelocity = new Vector2(moveDirection * movementSpeed, body.linearVelocity.y);
            }
        }
    }


    public float SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
        return movementSpeed;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}