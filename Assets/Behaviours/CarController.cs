using UnityEngine;
using UnityEngine.InputSystem;

// Drives the car forwards with physics forces while the up arrow is held,
// and rolls the wheels to match how fast the car is actually moving.
// Timing bonus: press the up arrow just as the car lands to get a speed boost.
// Attached to the Car prefab, which has a Rigidbody.
public class CarController : MonoBehaviour
{
    // Forward force applied to the Rigidbody while the up arrow is held.
    public float motorForce = 10f;

    // The four wheels; they spin to match how far the car drives.
    public Transform[] wheels;

    // Wheel radius in world units; controls how fast the wheels spin.
    public float wheelRadius = 0.5f;

    // Impulse added when the up arrow is pressed in time with a landing.
    public float boostForce = 10f;

    // How close (in seconds) the press and the landing must be to earn the boost.
    public float boostWindow = 0.2f;

    private Rigidbody _rigidbody;
    private float _lastLandTime = -999f;
    private float _lastPressTime = -999f;
    private bool _boostedThisLanding;

    // Awake runs before any physics callbacks, so the Rigidbody is ready for OnCollisionEnter.
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update runs every frame; read one-off key presses here so none are missed.
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _lastPressTime = Time.time;
            TryBoost();
        }
    }

    // FixedUpdate is called on the physics timestep; apply forces to the Rigidbody here.
    void FixedUpdate()
    {
        // Read the up arrow directly from the new Input System.
        if (Keyboard.current != null && Keyboard.current.upArrowKey.isPressed)
        {
            // Push the car along its "nose" direction. The Rigidbody's damping balances
            // this out at a natural top speed, so no manual speed cap is needed.
            _rigidbody.AddForce(transform.forward * motorForce, ForceMode.Force);
        }

        // Roll the wheels by the distance the car actually travels this step.
        float forwardSpeed = Vector3.Dot(_rigidbody.linearVelocity, transform.forward);
        RollWheels(forwardSpeed * Time.fixedDeltaTime);
    }

    // Called by the physics engine when the car starts touching something (e.g. the ground).
    void OnCollisionEnter(Collision collision)
    {
        _lastLandTime = Time.time;
        _boostedThisLanding = false;
        TryBoost();
    }

    // Gives a one-off boost when a press and a landing happen within boostWindow of each other.
    private void TryBoost()
    {
        // Only one boost per landing, and only if both events are recent.
        if (_boostedThisLanding) return;
        if (Time.time - _lastLandTime > boostWindow) return;
        if (Time.time - _lastPressTime > boostWindow) return;

        _rigidbody.AddForce(transform.forward * boostForce, ForceMode.Impulse);
        _boostedThisLanding = true;
    }

    // Spins each wheel around its axle by the arc length the car just travelled.
    private void RollWheels(float distance)
    {
        // A wheel of radius r turns (distance / r) radians when it rolls that distance.
        // Negated so the wheels roll in the direction of travel (spin around the axle the correct way).
        float degrees = -(distance / wheelRadius) * Mathf.Rad2Deg;
        foreach (Transform wheel in wheels)
        {
            // The wheel cylinder's local Y axis is its axle, so spin around that.
            wheel.Rotate(0f, degrees, 0f, Space.Self);
        }
    }
}
