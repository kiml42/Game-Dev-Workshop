using UnityEngine;

// Sits on each wheel, which is its own Rigidbody once hinged to the car body.
// Wheel-vs-ground collisions are reported to the wheel, not to the car, so this
// forwards them up to the CarController so it can still detect landings for the boost.
[RequireComponent(typeof(Rigidbody))]
public class WheelCollisionRelay : MonoBehaviour
{
    private CarController _car;

    void Awake()
    {
        // The CarController lives on a parent (the Car root).
        _car = GetComponentInParent<CarController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_car != null)
        {
            _car.WheelTouchedGround();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (_car != null)
        {
            _car.WheelLeftGround();
        }
    }
}
