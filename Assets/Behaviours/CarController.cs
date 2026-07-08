using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Drives the car with the arrow keys, using the WheelColliders:
//   Up/Down    — forward / reverse motor torque
//   Left/Right — steer the front wheels
// The physics engine handles traction and rolling; WheelVisual spins the meshes to match.
// Landing is detected via WheelCollider.isGrounded.
// Timing bonus: press the up arrow just as the car lands to get a speed boost.
// Attached to the Car prefab, which has a Rigidbody.
public class CarController : MonoBehaviour
{
    // Motor torque (N·m) applied to the drive wheels for forward and reverse.
    public float motorTorque = 100f;

    // Maximum steer angle (degrees) applied to the front wheels at full lock.
    public float maxSteerAngle = 30f;

    // Impulse added when the up arrow is pressed in time with a landing.
    public float boostForce = 10f;

    // How close (in seconds) the press and the landing must be to earn the boost.
    public float boostWindow = 0.2f;

    // Local-space centre of mass for the body. Kept low so drive torque doesn't
    // pitch the car over — raise/lower Y to tune how tippy it feels.
    public Vector3 centerOfMass = new Vector3(0f, -0.5f, 0f);

    private Rigidbody _rigidbody;
    private WheelCollider[] _wheels;
    private WheelCollider[] _frontWheels;
    private bool _wasGrounded = true;
    private float _lastLandTime = -999f;
    private float _lastPressTime = -999f;
    private bool _boostedThisLanding;

    // Awake runs before any physics callbacks so references are ready in time.
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _wheels = GetComponentsInChildren<WheelCollider>();

        // The front wheels sit ahead of the car's centre (local Z > 0); only they steer.
        List<WheelCollider> fronts = new List<WheelCollider>();
        foreach (WheelCollider wheel in _wheels)
        {
            if (wheel.transform.localPosition.z > 0f)
            {
                fronts.Add(wheel);
            }
        }
        _frontWheels = fronts.ToArray();

        // Drop the centre of mass low so the wheels' drive force can't flip the body.
        _rigidbody.centerOfMass = centerOfMass;
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

    // FixedUpdate is called on the physics timestep; drive and steer the wheels here.
    void FixedUpdate()
    {
        // Read the arrow keys as steady held inputs from the new Input System.
        float drive = 0f;
        float steer = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.upArrowKey.isPressed) drive += 1f;    // forward
            if (keyboard.downArrowKey.isPressed) drive -= 1f;  // reverse
            if (keyboard.leftArrowKey.isPressed) steer -= 1f;  // turn left
            if (keyboard.rightArrowKey.isPressed) steer += 1f; // turn right
        }

        // Drive torque goes to every wheel; steering only to the front wheels.
        foreach (WheelCollider wheel in _wheels)
        {
            wheel.motorTorque = drive * motorTorque;
        }
        foreach (WheelCollider wheel in _frontWheels)
        {
            wheel.steerAngle = steer * maxSteerAngle;
        }

        // Watch the wheels for the moment the car lands, so the boost can react to it.
        bool isGrounded = AnyWheelGrounded();
        if (isGrounded && !_wasGrounded)
        {
            _lastLandTime = Time.time;
            _boostedThisLanding = false;
            TryBoost();
        }
        _wasGrounded = isGrounded;
    }

    // True if at least one wheel is currently touching the ground.
    private bool AnyWheelGrounded()
    {
        foreach (WheelCollider wheel in _wheels)
        {
            if (wheel.isGrounded) return true;
        }
        return false;
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
