// Required namespaces and classes for the MovingPlatform script
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    // Movement speed of the platform
    public float Speed;

    // Start and end points for the platform's movement
    public Transform startPoint;
    public Transform endPoint;

    // Time the platform rests at each end point before moving again
    public float restTime;

    // Rigidbody2D component for the platform
    private Rigidbody2D rb;

    // Direction vector for the platform's movement
    private Vector2 direction;

    // The vector positions for the start and end points
    private Vector2 startPos;
    private Vector2 endPos;

    // The target position the platform is moving towards
    private Vector2 targetPos;

    // Timer to track resting time
    private float timer;

    // Flag to check if the platform is currently resting at an end point
    private bool isResting;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize start and end positions based on the assigned transforms or the platform's position
        startPos = startPoint != null ? (Vector2)startPoint.position : (Vector2)transform.position;
        endPos = endPoint != null ? (Vector2)endPoint.position : (Vector2)transform.position;

        // Ensure the Rigidbody2D component is attached to the platform
        rb = GetComponent<Rigidbody2D>();

        // Set the initial target position to the start position
        targetPos = endPos;
        direction = CalculateDirection(targetPos);

        // Initialize the resting state and timer
        isResting = false;
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the platform is resting, increment the timer until it reaches the rest time
        if (isResting)
        {
            if (timer >= restTime)
            {
                // Reset the timer and switch the target position
                timer = 0;
                isResting = false;
            }
            else
            {
                // Increment the timer while resting
                timer += Time.deltaTime;
            }
        }
        else
        {
            // Move the platform towards the target position if not resting
            rb.linearVelocity = new Vector2(direction.x * Speed, direction.y * Speed);

            // Check if the platform has reached the target position
            if (Vector2.Distance(rb.position, targetPos) < 0.1f)
            {
                // Switch the target position and reset the direction
                targetPos = targetPos == startPos ? endPos : startPos;
                direction = CalculateDirection(targetPos);
                rb.linearVelocity = Vector2.zero;
                isResting = true;
            }
        }
    }

    // Calculate the normalized direction vector towards the target position
    private Vector2 CalculateDirection(Vector2 target)
    {
        // Calculate the direction vector from the platform's current position to the target position
        Vector2 dir = target - rb.position;
        dir.Normalize();
        return dir;
    }

    // Handle collision with other objects, specifically characters
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has a Rigidbody2D component
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            // Parent the object to this platform to move with it
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<PlayerMovement>().isOnMovingPlatform = true;
            collision.gameObject.GetComponent<PlayerMovement>().movingPlatformBody = rb;
        }
    }

    // Handle when an object exits the platform's trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is a child of this platform
        if (collision.gameObject.transform.parent == transform)
        {
            // Unparent the object from the platform and reset its movement state
            collision.gameObject.transform.SetParent(null);
            collision.gameObject.GetComponent<PlayerMovement>().isOnMovingPlatform = false;
            collision.gameObject.GetComponent<PlayerMovement>().movingPlatformBody = null;
        }
    }

    // Draw Gizmos in the editor to visualize the start and end points of the platform
    private void OnDrawGizmosSelected()
    {
        // Ensure the start and end points are defined before drawing
        Vector2 currentStartPos = startPoint != null ? (Vector2)startPoint.position : (Application.isPlaying ? startPos : (Vector2)transform.position);
        Vector2 currentEndPos = endPoint != null ? (Vector2)endPoint.position : (Application.isPlaying ? endPos : (Vector2)transform.position);

        // Draw spheres at the start and end points
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

        // Draw the line from the platform's position to the start or end point if only one is defined
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
        // If neither is defined, Gizmos will show nothing
    }
}