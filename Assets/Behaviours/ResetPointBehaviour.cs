using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ResetPointBehaviour : MonoBehaviour
{
    private Vector3 _cubeStartPosition;
    private Rigidbody _cubeRigidbody;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = BuildPyramidMesh();
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().isTrigger = true;
    }

    void Start()
    {
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

    Mesh BuildPyramidMesh()
    {
        var mesh = new Mesh();

        var vertices = new Vector3[]
        {
            new Vector3(-0.5f, 0f, -0.5f),
            new Vector3( 0.5f, 0f, -0.5f),
            new Vector3( 0.5f, 0f,  0.5f),
            new Vector3(-0.5f, 0f,  0.5f),
            new Vector3( 0f,   1f,  0f),
        };

        var triangles = new int[]
        {
            0, 2, 1,  0, 3, 2,  // base
            0, 1, 4,             // front
            1, 2, 4,             // right
            2, 3, 4,             // back
            3, 0, 4,             // left
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
