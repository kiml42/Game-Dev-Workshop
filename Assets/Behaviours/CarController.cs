using UnityEngine;
using UnityEngine.InputSystem;

// Drives the car forwards with physics forces while the up arrow is held.
// The wheels are attached to the body with HingeJoints, so the physics engine
// rolls them from ground friction — there is no wheel-rolling code here.
// Timing bonus: press the up arrow just as the car lands to get a speed boost.
// Attached to the Car prefab, which has a Rigidbody.
public class CarController : MonoBehaviour
{
    // Forward force applied to the Rigidbody while the up arrow is held.
    public float motorForce = 10f;

    // Impulse added when the up arrow is pressed in time with a landing.
    public float boostForce = 10f;

    // How close (in seconds) the press and the landing must be to earn the boost.
    public float boostWindow = 0.2f;

    private Rigidbody _rigidbody;
    private int _wheelsOnGround;
    private float _lastLandTime = -999f;
    private float _lastPressTime = -999f;
    private bool _boostedThisLanding;

    // Awake runs before any physics callbacks so the Rigidbody is ready in time.
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
            // Push the car along its "nose" direction. The wheels roll along via their
            // HingeJoints, and the Rigidbody's damping settles it at a natural top speed.
            _rigidbody.AddForce(transform.forward * motorForce, ForceMode.Force);
        }
    }

    // Called by a wheel's WheelCollisionRelay when that wheel starts touching a surface.
    public void WheelTouchedGround()
    {
        // The moment the first wheel makes contact counts as "hitting the ground".
        if (_wheelsOnGround == 0)
        {
            _lastLandTime = Time.time;
            _boostedThisLanding = false;
            TryBoost();
        }
        _wheelsOnGround++;
    }

    // Called by a wheel's WheelCollisionRelay when that wheel stops touching a surface.
    public void WheelLeftGround()
    {
        _wheelsOnGround = Mathf.Max(0, _wheelsOnGround - 1);
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
}
