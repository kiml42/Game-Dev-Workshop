using UnityEngine;

public class WheelJoints : MonoBehaviour
{
    void Awake()
    {
        Rigidbody carBody = transform.parent.GetComponent<Rigidbody>();

        transform.SetParent(null);

        var rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();

        var joint = gameObject.AddComponent<HingeJoint>();
        joint.connectedBody = carBody;
        joint.axis = transform.InverseTransformDirection(Vector3.right);
        joint.autoConfigureConnectedAnchor = true;
    }
}
