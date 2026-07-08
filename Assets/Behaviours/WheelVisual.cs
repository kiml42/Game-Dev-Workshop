using UnityEngine;

// Moves and spins the visual wheel meshes to match their WheelCollider's simulated pose.
// The physics engine decides the rotation (the WheelCollider rolls around its X axle),
// so there is no manual rotation axis here — we just copy the collider's world pose.
// Attached to each wheel, alongside its WheelCollider.
[RequireComponent(typeof(WheelCollider))]
public class WheelVisual : MonoBehaviour
{
    private WheelCollider _wheelCollider;
    private Transform[] _visuals;
    private Quaternion[] _baseRotations;

    void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();

        // The visual meshes are the direct children (Hub and Tyre). Remember each one's
        // starting local rotation so we can add the collider's spin on top of it.
        int count = transform.childCount;
        _visuals = new Transform[count];
        _baseRotations = new Quaternion[count];
        for (int i = 0; i < count; i++)
        {
            _visuals[i] = transform.GetChild(i);
            _baseRotations[i] = _visuals[i].localRotation;
        }
    }

    // Update runs after the physics step has moved and rolled the WheelCollider this frame.
    void Update()
    {
        // Ask the WheelCollider where the wheel currently sits and how far it has rolled.
        _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        for (int i = 0; i < _visuals.Length; i++)
        {
            _visuals[i].position = position;
            // Combine the collider's world rotation with the mesh's original orientation,
            // so each mesh keeps its authored alignment and simply spins as the wheel rolls.
            _visuals[i].rotation = rotation * _baseRotations[i];
        }
    }
}
