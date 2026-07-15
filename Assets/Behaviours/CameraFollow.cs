using UnityEngine;

// Chase camera: stays behind and above a target (the car), following its heading, and looks at it.
// If no target is assigned in the Inspector, it finds the CarMovement in the scene automatically.
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;   // how far behind the car
    public float height = 4f;     // how far above the car
    public float followSpeed = 5f;

    void Start()
    {
        if (target == null)
        {
            CarMovement car = FindFirstObjectByType<CarMovement>();
            if (car != null)
                target = car.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Sit behind the car (opposite its forward) and above it.
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Always face the car.
        transform.LookAt(target.position);
    }
}
