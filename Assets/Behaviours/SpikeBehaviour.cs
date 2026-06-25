using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private Vector3 _cubeStartPosition;
    private Rigidbody _cubeRigidbody;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        var cube = FindFirstObjectByType<JumpBehaviour>();
        _cubeRigidbody = cube.GetComponent<Rigidbody>();
        _cubeStartPosition = _cubeRigidbody.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<JumpBehaviour>() == null) return;

        _cubeRigidbody.linearVelocity = Vector3.zero;
        _cubeRigidbody.transform.position = _cubeStartPosition;
    }
}
