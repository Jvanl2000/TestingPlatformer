using UnityEngine;

public class LagCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float smoothTime;
    [SerializeField] private float speed = 2;
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 5f;

    private Transform targetTransform;
    private Vector3 cameraZ; 
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        smoothTime = 1 / speed;

        if (target == null)
        {
            enabled = false;
            return;
        }
        cameraZ = new Vector3(0, 0, transform.position.z);
        targetTransform = target.transform;
    }

    private void LateUpdate()
    {
        if (target == null) return; // Ensure target is still valid

        Vector3 desiredPosition = new Vector3(targetTransform.position.x + xOffset, targetTransform.position.y + yOffset, cameraZ.z);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

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