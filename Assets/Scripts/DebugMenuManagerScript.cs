using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMenuManagerScript : MonoBehaviour {

    [SerializeField] InputActionReference DebugMenuToggle;
    [SerializeField] Canvas Canvas;

    public GameObject player;
    private PlayerMovement PlayerMovementScript;
    private PlayerJumping PlayerJumpingScript;
    private Rigidbody2D Rigidbody;

    public Camera Camera;
    private LagCamera LagCamera;

    public TMP_InputField PlayerMovementSpeedInput;
    public TMP_InputField PlayerJumpForceInput;
    public TMP_InputField PlayerGravityScaleInput;
    public TMP_InputField CameraSizeInput;
    public TMP_InputField CameraSpeedInput;
    public TMP_InputField CameraXOffsetInput;
    public TMP_InputField CameraYOffsetInput;

    private void Start() {

        PlayerMovementScript = player.GetComponent<PlayerMovement>();
        PlayerJumpingScript = player.GetComponent<PlayerJumping>();
        Rigidbody = player.GetComponent<Rigidbody2D>();
        Camera = Camera.GetComponent<Camera>();
        LagCamera = Camera.GetComponent<LagCamera>();
        Canvas.enabled = false;

        PlayerMovementSpeedInput.onEndEdit.AddListener(HandleMovementSpeedInput);
        PlayerJumpForceInput.onEndEdit.AddListener(HandleJumpForceInput);
        PlayerGravityScaleInput.onEndEdit.AddListener(HandleGravityScaleInput);
        CameraSizeInput.onEndEdit.AddListener(HandleCameraSizeInput);
        CameraSpeedInput.onEndEdit.AddListener(HandleCameraSpeedInput);
        CameraXOffsetInput.onEndEdit.AddListener(HandleCameraXOffsetInput);
        CameraYOffsetInput.onEndEdit.AddListener(HandleCameraYOffsetInput);

        PlayerMovementSpeedInput.text = PlayerMovementScript.GetMovementSpeed().ToString();
        PlayerJumpForceInput.text = PlayerJumpingScript.GetJumpForce().ToString();
        PlayerGravityScaleInput.text = Rigidbody.gravityScale.ToString();
        CameraSizeInput.text = Camera.orthographicSize.ToString();
        CameraSpeedInput.text = LagCamera.GetSpeed().ToString();
        CameraXOffsetInput.text = LagCamera.GetXOffset().ToString();
        CameraYOffsetInput.text = LagCamera.GetYOffset().ToString();
    }

    private void OnEnable()
    {
        DebugMenuToggle.action.performed += ToggleDebugMenu;
    }

    private void OnDisable()
    {
        DebugMenuToggle.action.performed -= ToggleDebugMenu;
    }

    private void ToggleDebugMenu(InputAction.CallbackContext context) {
        if (Canvas.enabled) { Canvas.enabled = false; }
        else { Canvas.enabled = true; }
    }

    private void HandleMovementSpeedInput(string input) {

        if (string.IsNullOrWhiteSpace(input)) {
            PlayerMovementSpeedInput.text = PlayerMovementScript.GetMovementSpeed().ToString();
            return;
        }

        if (float.TryParse(input, out float newMovementSpeed)) {
            if (PlayerMovementScript != null) {
                PlayerMovementSpeedInput.text = PlayerMovementScript.SetMovementSpeed(newMovementSpeed).ToString();
            }
        } else {
            PlayerMovementSpeedInput.text = PlayerMovementScript.GetMovementSpeed().ToString();
        }
    }

    private void HandleJumpForceInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            PlayerJumpForceInput.text = PlayerJumpingScript.GetJumpForce().ToString();
            return;
        }

        if (float.TryParse(input, out float newJumpForce))
        {
            if (PlayerMovementScript != null)
            {
                PlayerJumpForceInput.text = PlayerJumpingScript.SetJumpForce(newJumpForce).ToString();
            }
        }
        else
        {
            PlayerJumpForceInput.text = PlayerJumpingScript.GetJumpForce().ToString();
        }
    }

    private void HandleGravityScaleInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            PlayerGravityScaleInput.text = Rigidbody.gravityScale.ToString();
            return;
        }

        if (float.TryParse(input, out float newGravityScale))
        {
            if (PlayerMovementScript != null)
            {
                Rigidbody.gravityScale = newGravityScale;
                PlayerGravityScaleInput.text = newGravityScale.ToString();
            }
        }
        else
        {
            PlayerGravityScaleInput.text = Rigidbody.gravityScale.ToString();
        }
    }

    private void HandleCameraSizeInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            CameraSizeInput.text = Camera.orthographicSize.ToString();
            return;
        }

        if (float.TryParse(input, out float newCameraSize))
        {
            if (PlayerMovementScript != null)
            {
                Camera.orthographicSize = newCameraSize;
                CameraSizeInput.text = newCameraSize.ToString();
            }
        }
        else
        {
            CameraSizeInput.text = Camera.orthographicSize.ToString();
        }
    }

    private void HandleCameraXOffsetInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            CameraXOffsetInput.text = LagCamera.GetXOffset().ToString();
            return;
        }

        if (float.TryParse(input, out float newXOffset))
        {
            if (PlayerMovementScript != null)
            {
                CameraXOffsetInput.text = LagCamera.SetXOffset(newXOffset).ToString();
            }
        }
        else
        {
            CameraXOffsetInput.text = LagCamera.GetXOffset().ToString();
        }
    }

    private void HandleCameraYOffsetInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            CameraYOffsetInput.text = LagCamera.GetYOffset().ToString();
            return;
        }

        if (float.TryParse(input, out float newYOffset))
        {
            if (PlayerMovementScript != null)
            {
                CameraYOffsetInput.text = LagCamera.SetYOffset(newYOffset).ToString();
            }
        }
        else
        {
            CameraYOffsetInput.text = LagCamera.GetYOffset().ToString();
        }
    }

    private void HandleCameraSpeedInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            CameraSpeedInput.text = LagCamera.GetSpeed().ToString();
            return;
        }

        if (float.TryParse(input, out float newSpeed))
        {
            if (PlayerMovementScript != null)
            {
                CameraSpeedInput.text = LagCamera.SetSpeed(newSpeed).ToString();
            }
        }
        else
        {
            CameraSpeedInput.text = LagCamera.GetSpeed().ToString();
        }
    }

}
