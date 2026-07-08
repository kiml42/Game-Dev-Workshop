using System.Collections.Generic;
using UnityEngine;

// Scatters randomly-chosen prefabs across a ground area, each at a random heading.
//
// Setup: add this to the ground object itself. That object supplies:
//   - the area size, from its X and Z scale;
//   - the orientation, position and height, via its transform — so a tilted or rotated
//     ground tilts and rotates the whole scattered field (everything is placed in the
//     ground's local space, letting you drop several angled grounds each with their own
//     scatter).
public class PrefabScatterer : MonoBehaviour
{
    [Tooltip("Prefabs chosen from at random, one per scattered instance.")]
    public List<GameObject> prefabs = new List<GameObject>();

    [Tooltip("Number of instances to scatter.")]
    public int count = 20;

    [Tooltip("Fraction of the ground area to fill (1 = right to the edges, less leaves a margin).")]
    [Range(0f, 1f)]
    public float areaFill = 1f;

    [Tooltip("World size of the ground mesh per unit of scale. Unity's built-in Plane is " +
             "10 units per scale unit, so leave this at 10 for a Plane; use 1 for a Cube/Quad.")]
    public float sizePerScaleUnit = 10f;

    // Instances created by the most recent scatter, so we can clear them before re-scattering.
    private readonly List<GameObject> _spawned = new List<GameObject>();

    void Start()
    {
        Scatter();
    }

    // Also runnable from the component's context menu to preview in the editor.
    [ContextMenu("Scatter")]
    public void Scatter()
    {
        Clear();

        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogWarning($"{name}: no prefabs assigned to scatter.", this);
            return;
        }

        // This object is the ground: use its own transform for the area and frame.
        Transform ground = transform;

        // Half the ground's size along its own X and Z axes. sizePerScaleUnit converts the
        // mesh's scale into real world size (e.g. 10 for Unity's Plane).
        float halfX = ground.localScale.x * sizePerScaleUnit * 0.5f * areaFill;
        float halfZ = ground.localScale.z * sizePerScaleUnit * 0.5f * areaFill;

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            if (prefab == null) continue;

            // Pick a spot within the area, offset along the ground's own axes so the field
            // follows the ground's rotation. This object's position is the centre/height.
            Vector3 position = transform.position
                + ground.right * Random.Range(-halfX, halfX)
                + ground.forward * Random.Range(-halfZ, halfZ);

            // Random heading about the ground's up axis, keeping instances upright on it.
            Quaternion rotation = ground.rotation * Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            _spawned.Add(Instantiate(prefab, position, rotation));
        }
    }

    // Removes everything created by the previous scatter.
    public void Clear()
    {
        foreach (GameObject instance in _spawned)
        {
            if (instance == null) continue;
            if (Application.isPlaying)
            {
                Destroy(instance);
            }
            else
            {
                DestroyImmediate(instance);
            }
        }
        _spawned.Clear();
    }
}
