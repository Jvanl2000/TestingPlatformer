// Required namespaces and classes for the PlayerMovement script
using UnityEngine;
using UnityEngine.InputSystem;

// PlayerMovement script to handle player movement using Unity's Input System
public class PlayerMovement : MonoBehaviour
{
    // Get the Rigidbody2D component attached to the player
    private Rigidbody2D body;
    // Input action reference for movement
    // A serialized field allows you to assign this in the Unity Editor
    [SerializeField] private InputActionReference move;

    // Movement speed of the player
    [SerializeField] private float movementSpeed = 5f;

    // Variable to store the current movement direction
    private float moveDirection = 0f;
    // Flag to check if the player is on a moving platform
    public bool isOnMovingPlatform = false;
    // Reference to the Rigidbody2D of the moving platform, if applicable
    public Rigidbody2D movingPlatformBody;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component from the GameObject this script is attached to
        body = GetComponent<Rigidbody2D>();
    }

    // OnEnable is called when the script instance is enabled
    private void OnEnable()
    {
        // Enable the move action to start receiving input
        move.action.Enable();

        // Subscribe to the performed and canceled events of the move action
        move.action.performed += OnMovePerformed; // Subscribe to move performed
        move.action.canceled += OnMoveCanceled;   // Subscribe to move canceled
    }

    // OnDisable is called when the script instance is disabled
    private void OnDisable()
    {
        // Disable the move action to stop receiving input
        move.action.Disable();

        // Unsubscribe from the performed and canceled events of the move action
        move.action.performed -= OnMovePerformed; // Unsubscribe from move performed
        move.action.canceled -= OnMoveCanceled;  // Unsubscribe from move canceled
    }

    // Callback method for when the move action is performed (input received)
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read the value from the input action and update the moveDirection variable
        moveDirection = context.ReadValue<float>();
    }

    // Callback method for when the move action is canceled (input released)
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset the moveDirection to 0 when the input is canceled
        moveDirection = context.ReadValue<float>(); // Should be 0 when canceled for a standard axis
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the player has fallen below a certain y-coordinate (e.g., -50)
        if (transform.position.y <= -50)
        {
            // Reset the player's position to the origin (0, 0, 0) and reset the Rigidbody2D's velocity
            transform.position = Vector3.zero;
            if (body != null) body.linearVelocity = Vector2.zero;
        }
    }

    // FixedUpdate is called at a fixed interval and is used for physics updates
    private void FixedUpdate()
    {
        // If the body is null, return early to avoid null reference exceptions
        if (body != null)
        {
            // If the moveDirection is 0, set the linear velocity to 0 on the x-axis to stop horizontal movement
            if (isOnMovingPlatform && movingPlatformBody != null)
            {
                // If the player is on a moving platform, adjust the velocity based on the platform's velocity
                Vector2 platformVelocity = movingPlatformBody.linearVelocity;
                body.linearVelocity = new Vector2(moveDirection * movementSpeed + platformVelocity.x, body.linearVelocity.y);
            }
            else
            {
                // If the player is not on a moving platform, set the linear velocity based on the moveDirection and movementSpeed
                body.linearVelocity = new Vector2(moveDirection * movementSpeed, body.linearVelocity.y);
            }
        }
    }

    // Method to set the movement speed of the player
    public float SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
        return movementSpeed;
    }

    // Method to get the current movement speed of the player   
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}