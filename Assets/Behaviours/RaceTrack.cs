using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public Material trackMaterial;
    public Material startLineMaterial;
    public Material finishLineMaterial;
    public Material racerMaterial;
    public int racerCount = 3;

    // Waypoints going clockwise around the right straight start
    private static readonly Vector3[] Waypoints =
    {
        new Vector3( 46, 0.5f,  40),
        new Vector3( 43, 0.5f,  52),
        new Vector3(  0, 0.5f,  52),
        new Vector3(-43, 0.5f,  52),
        new Vector3(-46, 0.5f,  40),
        new Vector3(-46, 0.5f,   0),
        new Vector3(-46, 0.5f, -40),
        new Vector3(-43, 0.5f, -52),
        new Vector3(  0, 0.5f, -52),
        new Vector3( 43, 0.5f, -52),
        new Vector3( 46, 0.5f, -40),
        new Vector3( 46, 0.5f,   0),
    };

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
        Object.Destroy(startLine.GetComponent<Collider>());
        if (startLineMaterial != null)
            startLine.GetComponent<Renderer>().material = startLineMaterial;

        // Finish line (just behind the start line)
        var finishLine = CreateSegment("Finish Line", new Vector3(46, 0.5f, -47), new Vector3(12, 0.01f, 0.5f));
        Object.Destroy(finishLine.GetComponent<Collider>());
        if (finishLineMaterial != null)
            finishLine.GetComponent<Renderer>().material = finishLineMaterial;

        // AI racers
        for (int i = 0; i < racerCount; i++)
        {
            var racer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            racer.name = "AI Racer " + (i + 1);
            racer.transform.localScale = new Vector3(1, 0.5f, 2);
            racer.transform.position = new Vector3(46, 1.5f, -43 - (i + 1) * 4f);
            racer.transform.rotation = Quaternion.Euler(0, 0, 0);

            var rb = racer.AddComponent<Rigidbody>();
            rb.mass = 1f;

            if (racerMaterial != null)
                racer.GetComponent<Renderer>().material = racerMaterial;

            var ai = racer.AddComponent<AIRacer>();
            ai.Init(Waypoints);
        }

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
