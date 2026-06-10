using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    public float moveForce = 20f;
    public float turnTorque = 10f;

    private Rigidbody rb;
    private InputAction moveAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Player/Move");
    }

    void OnEnable() => moveAction.Enable();
    void OnDisable() => moveAction.Disable();

    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        float forward = input.y;
        float speed = forward < 0 ? moveForce * 0.75f : moveForce;
        rb.AddForce(transform.forward * forward * speed);
        rb.AddTorque(transform.up * input.x * turnTorque);
    }
}
