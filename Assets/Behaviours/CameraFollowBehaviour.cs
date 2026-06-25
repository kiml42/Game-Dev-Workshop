using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -8f);

    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
