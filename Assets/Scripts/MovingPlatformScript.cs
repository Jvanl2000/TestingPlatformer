using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float Speed;

    public Transform startPoint;
    public Transform endPoint;

    public float restTime;
    private Rigidbody2D rb;
    private Vector2 direction;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 targetPos;

    private float timer;
    private bool isResting;

    private void Start()
    {
        startPos = startPoint != null ? (Vector2)startPoint.position : (Vector2)transform.position;
        endPos = endPoint != null ? (Vector2)endPoint.position : (Vector2)transform.position;
        rb = GetComponent<Rigidbody2D>();

        targetPos = endPos;
        direction = CalculateDirection(targetPos);


        isResting = false;
        timer = 0;
    }

    private void Update()
    {
        if (isResting)
        {
            if (timer >= restTime)
            {
                timer = 0;
                isResting = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(direction.x * Speed, direction.y * Speed);
            if (Vector2.Distance(rb.position, targetPos) < 0.1f)
            {
                targetPos = targetPos == startPos ? endPos : startPos;
                direction = CalculateDirection(targetPos);
                rb.linearVelocity = Vector2.zero;
                isResting = true;
            }
        }
    }

    private Vector2 CalculateDirection(Vector2 target)
    {
        Vector2 dir = target - rb.position;
        dir.Normalize();
        return dir;
    }

    // Ensure the platform has a Collider2D marked as Trigger and a Kinematic Rigidbody2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has a Rigidbody2D (likely a character)
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            // Parent the colliding object to the platform
            // Using 'true' keeps the child's world position relative to the parent
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<PlayerMovement>().isOnMovingPlatform = true;
            collision.gameObject.GetComponent<PlayerMovement>().movingPlatformBody = rb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object exiting was parented to this platform
        if (collision.gameObject.transform.parent == transform)
        {
            // Unparent the object
            collision.gameObject.transform.SetParent(null);
            collision.gameObject.GetComponent<PlayerMovement>().isOnMovingPlatform = false;
            collision.gameObject.GetComponent<PlayerMovement>().movingPlatformBody = null;
        }
    }

    // Visual aids in the editor
    private void OnDrawGizmosSelected()
    {
        // Use the current start/end positions if points are null for Gizmos
        Vector2 currentStartPos = startPoint != null ? (Vector2)startPoint.position : (Application.isPlaying ? startPos : (Vector2)transform.position);
        Vector2 currentEndPos = endPoint != null ? (Vector2)endPoint.position : (Application.isPlaying ? endPos : (Vector2)transform.position);


        if (startPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(currentStartPos, 0.3f);
        }

        if (endPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentEndPos, 0.3f);
        }

        // Draw the line between start and end points if both are defined
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(currentStartPos, currentEndPos);
        }
        // Optional: Draw line even if only one point is defined, relative to current position
        else if (startPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(currentStartPos, transform.position);
        }
        else if (endPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, currentEndPos);
        }
        // If neither is defined, Gizmos will show nothing, which is fine.
    }
}