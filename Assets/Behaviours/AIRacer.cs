using UnityEngine;

public class AIRacer : MonoBehaviour
{
    public float moveForce = 50f;
    public float turnTorque = 20f;
    public float maxSpeed = 8f;
    public float waypointRadius = 5f;

    private Rigidbody rb;
    private Vector3[] waypoints;
    private int currentWaypoint;

    public void Init(Vector3[] trackWaypoints)
    {
        waypoints = trackWaypoints;
        currentWaypoint = 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Vector3 target = waypoints[currentWaypoint];
        target.y = transform.position.y;

        float distance = Vector3.Distance(transform.position, target);
        if (distance < waypointRadius)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;

        Vector3 toTarget = (target - transform.position).normalized;
        float steer = Vector3.Cross(transform.forward, toTarget).y;
        rb.AddTorque(Vector3.up * steer * turnTorque);

        if (rb.linearVelocity.magnitude < maxSpeed)
            rb.AddForce(transform.forward * moveForce);
    }
}
