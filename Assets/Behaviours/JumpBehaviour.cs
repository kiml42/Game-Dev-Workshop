using UnityEngine;
using UnityEngine.InputSystem;

public class JumpBehaviour : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float gravityMultiplier = 3f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply extra gravity on top of Unity's default
        _rigidbody.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            // v = sqrt(2 * g_eff * h) — gives the same apex regardless of gravity scale
            float effectiveGravity = Physics.gravity.magnitude * gravityMultiplier;
            float jumpVelocity = Mathf.Sqrt(2f * effectiveGravity * jumpHeight);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, jumpVelocity, _rigidbody.linearVelocity.z);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }
}
