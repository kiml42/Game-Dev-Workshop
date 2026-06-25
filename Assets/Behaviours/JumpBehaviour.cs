using UnityEngine;
using UnityEngine.InputSystem;

public class JumpBehaviour : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float gravityMultiplier = 3f;

    private Rigidbody _rigidbody;
    private float _jumpRotationSpeed;
    private float _rotationRemaining;
    private bool _isJumping;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
        _rigidbody.linearVelocity = new Vector3(10f, _rigidbody.linearVelocity.y, _rigidbody.linearVelocity.z);
    }

    void Update()
    {
        bool jumpHeld = Keyboard.current.spaceKey.isPressed
            || Keyboard.current.upArrowKey.isPressed
            || Keyboard.current.wKey.isPressed
            || Mouse.current.leftButton.isPressed;

        if (jumpHeld && IsGrounded())
        {
            float effectiveGravity = Physics.gravity.magnitude * gravityMultiplier;
            float jumpVelocity = Mathf.Sqrt(2f * effectiveGravity * jumpHeight);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, jumpVelocity, _rigidbody.linearVelocity.z);

            // 180 degrees spread evenly over the full jump arc
            float jumpDuration = 2f * jumpVelocity / effectiveGravity;
            _jumpRotationSpeed = 180f / jumpDuration;
            _rotationRemaining = 180f;
            _isJumping = true;
        }

        if (_isJumping)
        {
            float step = Mathf.Min(_jumpRotationSpeed * Time.deltaTime, _rotationRemaining);
            transform.Rotate(Vector3.forward, -step, Space.World);
            _rotationRemaining -= step;

            if (_rotationRemaining <= 0f)
            {
                SnapRotation();
                _isJumping = false;
            }
        }
    }

    void SnapRotation()
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = Mathf.Round(euler.z / 90f) * 90f;
        transform.eulerAngles = euler;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }
}
