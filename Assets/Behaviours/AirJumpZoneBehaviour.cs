using UnityEngine;

public class AirJumpZoneBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        var jump = other.GetComponent<JumpBehaviour>();
        if (jump != null) jump.SetInAirJumpZone(true);
    }

    void OnTriggerExit(Collider other)
    {
        var jump = other.GetComponent<JumpBehaviour>();
        if (jump != null) jump.SetInAirJumpZone(false);
    }
}
