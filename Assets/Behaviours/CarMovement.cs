using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    public float moveForce = 20f;

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
        float forward = moveAction.ReadValue<Vector2>().y;
        float speed = forward < 0 ? moveForce * 0.75f : moveForce;
        rb.AddForce(transform.forward * forward * speed);
    }
}
