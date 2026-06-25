using UnityEngine;

public class GravityFlipZoneBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        var jump = other.GetComponent<JumpBehaviour>();
        if (jump != null) jump.FlipGravity();
    }
}
