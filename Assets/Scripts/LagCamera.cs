// Required namespaces and classes for the LagCamera script
using UnityEngine;

// LagCamera script to smoothly follow a target with a lag effect
public class LagCamera : MonoBehaviour
{
    // Serialized field to assign the target GameObject in the Unity Editor
    [SerializeField] private GameObject target;

    // Smooth time for the camera movement, calculated based on speed
    private float smoothTime;

    // Speed of the camera movement, can be adjusted in the Unity Editor
    [SerializeField] private float speed = 2;

    // Offsets for the camera position relative to the target
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 5f;

    // Private variables to store the target's transform and camera's Z position
    private Transform targetTransform;
    private Vector3 cameraZ; 
    private Vector3 velocity = Vector3.zero;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the camera's Z position and calculate the smooth time based on speed
        smoothTime = 1 / speed;

        // Check if the target is assigned; if not, disable the script
        if (target == null)
        {
            enabled = false;
            return;
        }

        // Set the camera's Z position to match the target's Z position
        cameraZ = new Vector3(0, 0, transform.position.z);
        targetTransform = target.transform;
    }

    // LateUpdate is called after all Update methods have been called
    private void LateUpdate()
    {
        // If the target is not assigned, do nothing
        if (target == null) return;

        // Calculate the desired position for the camera based on the target's position and offsets
        Vector3 desiredPosition = new Vector3(targetTransform.position.x + xOffset, targetTransform.position.y + yOffset, cameraZ.z);

        // Smoothly move the camera towards the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    // Public methods to set and get the speed and offsets for the camera movement
    public float SetSpeed(float time)
    {
        speed = time; smoothTime = 1 / speed; return speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float SetXOffset(float x)
    {
        xOffset = x; return xOffset;
    }

    public float GetXOffset()
    {
        return xOffset;
    }

    public float SetYOffset(float y)
    {
        yOffset = y; return yOffset;
    }

    public float GetYOffset()
    {
        return yOffset;
    }

}