using System.Collections.Generic;
using UnityEngine;

// Scatters randomly-chosen prefabs across a ground area, each at a random heading.
// Runs at runtime only (on Start) — it never creates objects in the editor.
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

    [Tooltip("On: fully random 3D orientation (instances can tumble). Off: random heading " +
             "only, keeping instances upright on the ground.")]
    public bool randomOrientation = false;

    [Tooltip("Smallest size multiplier applied to a scattered instance.")]
    public float minScale = 0.8f;

    [Tooltip("Largest size multiplier applied to a scattered instance.")]
    public float maxScale = 1.2f;

    // Shared parent for every scatterer's instances, so they don't clutter the hierarchy.
    // Created lazily the first time any scatterer needs it.
    private static Transform _container;

    void Start()
    {
        Scatter();
    }

    public void Scatter()
    {
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

        Transform parent = GetContainer();

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            if (prefab == null) continue;

            // Pick a spot within the area, offset along the ground's own axes so the field
            // follows the ground's rotation. This object's position is the centre/height.
            Vector3 position = transform.position
                + ground.right * Random.Range(-halfX, halfX)
                + ground.forward * Random.Range(-halfZ, halfZ);

            // Either a fully random tumble, or just a random heading about the ground's up
            // axis (upright on the surface).
            Quaternion rotation = randomOrientation
                ? Random.rotation
                : ground.rotation * Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GameObject instance = Instantiate(prefab, position, rotation, parent);

            // Randomise the overall size, scaling the prefab's own scale uniformly.
            instance.transform.localScale *= Random.Range(minScale, maxScale);
        }
    }

    // Returns the shared container, creating it on first use. The == null check also catches
    // the case where a previous scene's container was destroyed (Unity reports it as null).
    private static Transform GetContainer()
    {
        if (_container == null)
        {
            _container = new GameObject("Scattered Objects").transform;
        }
        return _container;
    }
}
