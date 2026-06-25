using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 5f;

    private Transform target;

    private void Start()
    {
        var car = GameObject.Find("Car");
        if (car != null)
            target = car.transform;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up);
    }
}
