using UnityEngine;

// Smoothly follows a target (the car) from a fixed offset, keeping it in view.
// Attached to the Main Camera.
public class CameraFollow : MonoBehaviour
{
    // What to follow. If left empty, the car (its CarController) in the scene is found automatically.
    public Transform target;

    // Camera position relative to the target, in world space (behind and above by default).
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    // How long, in seconds, the camera takes to catch up to the target. Larger = lazier.
    public float smoothTime = 0.2f;

    private Vector3 _velocity;

    void Start()
    {
        // Fall back to the car in the scene if no target was assigned in the Inspector.
        if (target == null)
        {
            CarController car = FindAnyObjectByType<CarController>();
            if (car != null)
            {
                target = car.transform;
            }
        }
    }

    // LateUpdate runs after the car has moved this frame, so the camera tracks its final position.
    void LateUpdate()
    {
        if (target == null) return;

        // Ease towards the spot behind the car, then turn to look at it.
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothTime);
        transform.LookAt(target);
    }
}
