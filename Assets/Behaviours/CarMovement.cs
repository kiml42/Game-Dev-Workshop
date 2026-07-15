using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    public float moveForce = 20f;
    public float turnTorque = 10f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 1f;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction jumpAction;
    private bool jumpRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        // Capture the button press in Update so we don't miss it between physics steps.
        if (jumpAction.WasPressedThisFrame() && IsGrounded())
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        float forward = input.y;
        float speed = forward < 0 ? moveForce * 0.75f : moveForce;
        rb.AddForce(transform.forward * forward * speed);
        rb.AddTorque(transform.up * input.x * turnTorque);

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    // Grounded if something other than the car's own body is just below us.
    private bool IsGrounded()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, groundCheckDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.rigidbody != rb)
                return true;
        }
        return false;
    }
}
