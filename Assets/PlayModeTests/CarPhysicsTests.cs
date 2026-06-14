using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CarPhysicsTests
{
    private const string CarPrefabPath = "Assets/Prefabs/Car/Car.prefab";

    [UnityTest]
    public IEnumerator CarMovesWhenForceIsApplied()
    {
        var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        var car = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var rb = car.GetComponent<Rigidbody>();
        Assume.That(rb, Is.Not.Null, "Skipping: Rigidbody missing (caught by edit mode tests)");

        Vector3 startPosition = car.transform.position;

        rb.AddForce(Vector3.forward * 500f, ForceMode.Impulse);

        // Wait two physics frames for the force to take effect
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual(startPosition, car.transform.position,
            "Car did not move after applying a force. Check that the Rigidbody is not Kinematic and the car has a Collider.");

        Object.Destroy(car);
    }

    [UnityTest]
    public IEnumerator CarFallsUnderGravity()
    {
        var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        // Spawn above the ground so gravity has room to act
        var car = Object.Instantiate(prefab, new Vector3(0f, 10f, 0f), Quaternion.identity);
        var rb = car.GetComponent<Rigidbody>();
        Assume.That(rb, Is.Not.Null, "Skipping: Rigidbody missing (caught by edit mode tests)");

        float startY = car.transform.position.y;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.Less(car.transform.position.y, startY,
            "Car did not fall under gravity. Check that Use Gravity is enabled on the Rigidbody.");

        Object.Destroy(car);
    }
}
