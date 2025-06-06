using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{

    [SerializeField] private InputActionReference jump;
    private Rigidbody2D body;

    [SerializeField] private float jumpForce;

    [SerializeField] private Vector2 footSize = new Vector2(1f, 0.2f);
    [SerializeField] private float footDistance = 0.5f;

    private bool isGrounded;

    private void Start()
    {
        isGrounded = false;
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        jump.action.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        jump.action.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        isGrounded = CheckIsGrounded();
    }

    private bool CheckIsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(body.position, footSize, 0, Vector2.down, footDistance);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IsGround>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public float SetJumpForce(float value)
    {
        jumpForce = value;
        return jumpForce;
    }
}