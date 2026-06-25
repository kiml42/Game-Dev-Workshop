using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GravityFlipZoneTests
{
    private Vector3 _originalGravity;

    [SetUp]
    public void SetUp()
    {
        _originalGravity = Physics.gravity;
    }

    [TearDown]
    public void TearDown()
    {
        Physics.gravity = _originalGravity;
    }

    private GameObject CreateCube()
    {
        var cube = new GameObject("Cube");
        cube.AddComponent<BoxCollider>();
        cube.AddComponent<Rigidbody>();
        cube.AddComponent<JumpBehaviour>();
        return cube;
    }

    private GameObject CreateFlipZone(Vector3 position)
    {
        var zone = new GameObject("GravityFlipZone");
        zone.transform.position = position;
        var col = zone.AddComponent<BoxCollider>();
        col.size = Vector3.one * 2f;
        zone.AddComponent<GravityFlipZoneBehaviour>();
        return zone;
    }

    [UnityTest]
    public IEnumerator GravityFlipsWhenCubeEntersZone()
    {
        var cube = CreateCube();
        var zone = CreateFlipZone(new Vector3(0f, 10f, 0f));

        cube.transform.position = new Vector3(0f, 10f, 0f);

        yield return new WaitForFixedUpdate();

        Assert.AreEqual(-_originalGravity.y, Physics.gravity.y, 0.001f,
            "Gravity should be flipped after entering the zone.");

        Object.Destroy(cube);
        Object.Destroy(zone);
    }

    [UnityTest]
    public IEnumerator GravityFlipsTwiceBackToNormal()
    {
        var cube = CreateCube();
        var zone = CreateFlipZone(new Vector3(0f, 10f, 0f));

        cube.transform.position = new Vector3(0f, 10f, 0f);
        yield return new WaitForFixedUpdate();

        cube.transform.position = new Vector3(0f, 20f, 0f);
        yield return new WaitForFixedUpdate();
        cube.transform.position = new Vector3(0f, 10f, 0f);
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(_originalGravity.y, Physics.gravity.y, 0.001f,
            "Gravity should be back to normal after entering the zone twice.");

        Object.Destroy(cube);
        Object.Destroy(zone);
    }
}
