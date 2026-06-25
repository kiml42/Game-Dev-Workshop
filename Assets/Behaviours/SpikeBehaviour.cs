using System.Collections;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private Vector3 _cubeStartPosition;
    private Rigidbody _cubeRigidbody;
    private Renderer _cubeRenderer;
    private Vector3 _initialGravity;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        var cube = FindFirstObjectByType<JumpBehaviour>();
        _cubeRigidbody = cube.GetComponent<Rigidbody>();
        _cubeRenderer = cube.GetComponent<Renderer>();
        _cubeStartPosition = _cubeRigidbody.transform.position;
        _initialGravity = Physics.gravity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<JumpBehaviour>() == null) return;

        StartCoroutine(ResetSequence());
    }

    IEnumerator ResetSequence()
    {
        _cubeRenderer.enabled = false;
        _cubeRigidbody.linearVelocity = Vector3.zero;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        Physics.gravity = _initialGravity;
        _cubeRigidbody.transform.position = _cubeStartPosition;
        _cubeRenderer.enabled = true;
    }
}
