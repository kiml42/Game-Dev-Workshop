using UnityEngine;

// Simple enemy behaviour: the enemy has a spherical vision range around it.
// While the car is inside that sphere, the enemy walks toward it. Otherwise it does nothing.
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float visionRange = 10f;
    public float moveSpeed = 3f;

    void Start()
    {
        if (target == null)
        {
            CarMovement car = FindFirstObjectByType<CarMovement>();
            if (car != null)
                target = car.transform;
        }
    }

    void Update()
    {
        if (target == null)
            return;

        // Only react while the car is inside the vision sphere.
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > visionRange)
            return;

        // Walk toward the car, staying on the ground plane.
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.0001f)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    // Visualise the vision sphere in the Scene view.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
