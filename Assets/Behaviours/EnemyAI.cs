using UnityEngine;

// Simple enemy behaviour: the enemy has a spherical vision range around it.
// While the car is inside that sphere, the enemy walks toward it using physics
// (so walls block it). Otherwise it does nothing.
[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float visionRange = 10f;
    public float moveSpeed = 3f;
    public float turnSpeed = 180f;   // degrees per second

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (target == null)
        {
            CarMovement car = FindFirstObjectByType<CarMovement>();
            if (car != null)
                target = car.transform;
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        // Out of range (or no target): stop horizontal movement, keep falling under gravity.
        if (target == null || Vector3.Distance(transform.position, target.position) > visionRange)
        {
            rb.linearVelocity = new Vector3(0f, velocity.y, 0f);
            return;
        }

        // Direction to the car, flattened to the ground plane.
        Vector3 direction = target.position - rb.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.0001f)
            return;

        direction.Normalize();

        // Move via the Rigidbody so collisions are respected. Preserve vertical velocity for gravity.
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, velocity.y, direction.z * moveSpeed);

        // Turn to face the car via physics rotation.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
    }

    // Visualise the vision sphere in the Scene view.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
