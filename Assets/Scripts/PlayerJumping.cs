// Required namespaces and classes for the PlayerJumping script
using UnityEngine;
using UnityEngine.InputSystem;

// PlayerJumping script to handle player jumping using Unity's Input System
public class PlayerJumping : MonoBehaviour
{
    // Input action reference for jumping
    [SerializeField] private InputActionReference jump;

    // Get the Rigidbody2D component attached to the player
    private Rigidbody2D body;

    // Jump force applied when the player jumps
    [SerializeField] private float jumpForce;

    // Size and distance for the ground check using a box cast
    [SerializeField] private Vector2 footSize = new Vector2(1f, 0.2f);
    // Distance to check for ground beneath the player
    [SerializeField] private float footDistance = 0.5f;

    // Flag to check if the player is grounded
    private bool isGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the isGrounded flag and get the Rigidbody2D component
        isGrounded = false;
        body = GetComponent<Rigidbody2D>();
    }

    // OnEnable is called when the script instance is enabled
    private void OnEnable()
    {
        // Enable the jump action to start receiving input
        jump.action.performed += OnJumpPerformed;
    }

    // OnDisable is called when the script instance is disabled
    private void OnDisable()
    {
        // Disable the jump action to stop receiving input
        jump.action.performed -= OnJumpPerformed;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the player is grounded by performing a box cast downwards
        isGrounded = CheckIsGrounded();
    }

    // Method to check if the player is grounded using a box cast
    private bool CheckIsGrounded()
    {
        // Perform a box cast downwards to check for ground beneath the player
        RaycastHit2D[] hits = Physics2D.BoxCastAll(body.position, footSize, 0, Vector2.down, footDistance);

        // Iterate through the hits to see if any collider belongs to the ground
        for (int i = 0; i < hits.Length; i++)
        {
            // Check if the collider is not null and has the IsGround component
            RaycastHit2D hit = hits[i];
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IsGround>() != null)
            {
                return true;
            }
        }

        return false;
    }

    // Callback method for when the jump action is performed (input received)
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Check if the player is grounded before allowing the jump
        if (isGrounded)
        {
            // Apply an impulse force upwards to the player's Rigidbody2D to make them jump
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Getter for the jumpForce variable
    public float GetJumpForce()
    {
        return jumpForce;
    }

    // Setter for the jumpForce variable, allowing to set a new jump force value
    public float SetJumpForce(float value)
    {
        jumpForce = value;
        return jumpForce;
    }
}