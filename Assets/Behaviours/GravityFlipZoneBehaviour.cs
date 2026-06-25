using UnityEngine;

public class GravityFlipZoneBehaviour : MonoBehaviour
{
    private float _lastFlipTime = -10f;

    void OnTriggerEnter(Collider other)
    {
        if (Time.unscaledTime - _lastFlipTime < 1f) return;

        var jump = other.GetComponent<JumpBehaviour>();
        if (jump == null) return;

        jump.FlipGravity();
        _lastFlipTime = Time.unscaledTime;
    }
}
