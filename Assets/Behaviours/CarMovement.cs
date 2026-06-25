using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    public float moveForce = 50f;
    public float turnTorque = 20f;
    public float jumpForce = 8f;
    public float maxSpeed = 15f;

    private Rigidbody rb;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && IsGrounded())
            jumpRequested = true;
    }

    private void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float forward = (keyboard.upArrowKey.isPressed ? 1f : 0f) - (keyboard.downArrowKey.isPressed ? 1f : 0f);
        float turn = (keyboard.rightArrowKey.isPressed ? 1f : 0f) - (keyboard.leftArrowKey.isPressed ? 1f : 0f);

        if (rb.linearVelocity.magnitude < maxSpeed)
            rb.AddForce(transform.forward * forward * moveForce);
        rb.AddTorque(Vector3.up * turn * turnTorque);

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }
}
