using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public Material trackMaterial;
    public Material startLineMaterial;

    private void Awake()
    {
        // Rectangular track: two long straights, two short straights, four corners
        // Track is 12 units wide. Outer bounds: 104 x 64. Inner gap: 80 x 40.

        // Long straights (top and bottom)
        CreateSegment("Straight Top",    new Vector3(0, 0,  52), new Vector3(80, 0.5f, 12));
        CreateSegment("Straight Bottom", new Vector3(0, 0, -52), new Vector3(80, 0.5f, 12));

        // Short straights (left and right)
        CreateSegment("Straight Left",  new Vector3(-46, 0, 0), new Vector3(12, 0.5f, 92));
        CreateSegment("Straight Right", new Vector3( 46, 0, 0), new Vector3(12, 0.5f, 92));

        // Corners
        CreateSegment("Corner TL", new Vector3(-46, 0,  52), new Vector3(12, 0.5f, 12));
        CreateSegment("Corner TR", new Vector3( 46, 0,  52), new Vector3(12, 0.5f, 12));
        CreateSegment("Corner BL", new Vector3(-46, 0, -52), new Vector3(12, 0.5f, 12));
        CreateSegment("Corner BR", new Vector3( 46, 0, -52), new Vector3(12, 0.5f, 12));

        // Starting line
        var startLine = CreateSegment("Starting Line", new Vector3(46, 0.5f, -43), new Vector3(12, 0.01f, 0.5f));
        if (startLineMaterial != null)
            startLine.GetComponent<Renderer>().material = startLineMaterial;

        // Outer walls (track outer edge: x ±52, z ±58)
        CreateWall("Wall Outer Top",    new Vector3(  0, 1,  58), new Vector3(104, 2, 1));
        CreateWall("Wall Outer Bottom", new Vector3(  0, 1, -58), new Vector3(104, 2, 1));
        CreateWall("Wall Outer Left",   new Vector3(-52, 1,   0), new Vector3(1, 2, 116));
        CreateWall("Wall Outer Right",  new Vector3( 52, 1,   0), new Vector3(1, 2, 116));

        // Inner walls (track inner edge: x ±40, z ±46)
        CreateWall("Wall Inner Top",    new Vector3(  0, 1,  46), new Vector3(80, 2, 1));
        CreateWall("Wall Inner Bottom", new Vector3(  0, 1, -46), new Vector3(80, 2, 1));
        CreateWall("Wall Inner Left",   new Vector3(-40, 1,   0), new Vector3(1, 2, 92));
        CreateWall("Wall Inner Right",  new Vector3( 40, 1,   0), new Vector3(1, 2, 92));
    }

    private GameObject CreateSegment(string segmentName, Vector3 position, Vector3 scale)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = segmentName;
        obj.transform.SetParent(transform);
        obj.transform.position = position;
        obj.transform.localScale = scale;

        if (trackMaterial != null)
            obj.GetComponent<Renderer>().material = trackMaterial;

        return obj;
    }

    private void CreateWall(string wallName, Vector3 position, Vector3 scale)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = wallName;
        obj.transform.SetParent(transform);
        obj.transform.position = position;
        obj.transform.localScale = scale;
    }
}
