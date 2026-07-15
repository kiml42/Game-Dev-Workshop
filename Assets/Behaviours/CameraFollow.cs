using UnityEngine;

// Keeps the camera trailing a target (the car) at a fixed offset.
// If no target is assigned in the Inspector, it finds the CarMovement in the scene automatically.
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;

    // Offset from the target, captured from the camera's starting position so the
    // scene's existing camera angle is preserved.
    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            CarMovement car = FindFirstObjectByType<CarMovement>();
            if (car != null)
                target = car.transform;
        }

        if (target != null)
            offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
