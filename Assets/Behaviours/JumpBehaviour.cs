using UnityEngine;
using UnityEngine.InputSystem;

public class JumpBehaviour : MonoBehaviour
{
    public float jumpForce = 5f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
