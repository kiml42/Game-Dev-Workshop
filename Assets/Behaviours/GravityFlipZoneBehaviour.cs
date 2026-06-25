using UnityEngine;

public class GravityFlipZoneBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var jump = other.GetComponent<JumpBehaviour>();
        if (jump != null) jump.FlipGravity();
    }
}
