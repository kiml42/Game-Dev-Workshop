using UnityEngine;
using UnityEngine.InputSystem;

public class JumpBehaviour : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float gravityMultiplier = 3f;

    private Rigidbody _rigidbody;
    private float _jumpRotationSpeed;
    private bool _isJumping;
    private bool _wasGrounded;
    private bool _inAirJumpZone;

    public void SetInAirJumpZone(bool value) => _inAirJumpZone = value;

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

        if (jumpHeld && (IsGrounded() || _inAirJumpZone))
        {
            float effectiveGravity = Physics.gravity.magnitude * gravityMultiplier;
            float jumpVelocity = Mathf.Sqrt(2f * effectiveGravity * jumpHeight);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, jumpVelocity, _rigidbody.linearVelocity.z);

            float jumpDuration = 2f * jumpVelocity / effectiveGravity;
            _jumpRotationSpeed = 180f / jumpDuration;
            _isJumping = true;
        }

        bool grounded = IsGrounded();

        if (!grounded)
        {
            transform.Rotate(Vector3.forward, -_jumpRotationSpeed * Time.deltaTime, Space.World);
        }
        else if (!_wasGrounded)
        {
            SnapRotation();
        }

        _wasGrounded = grounded;
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
