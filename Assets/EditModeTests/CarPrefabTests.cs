using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class CarPrefabTests
{
    private const string CarPrefabPath = "Assets/Prefabs/Car/Car.prefab";
    private const string WheelPrefabPath = "Assets/Prefabs/Car/Wheel.prefab";

    [Test]
    public void CarPrefabHasRigidbody()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        var rb = prefab.GetComponent<Rigidbody>();
        Assert.IsNotNull(rb, "Car prefab is missing a Rigidbody component. Add one in the Inspector so the car can move with physics.");
    }

    [Test]
    public void CarPrefabHasCollider()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        var collider = prefab.GetComponentInChildren<Collider>();
        Assert.IsNotNull(collider, "Car prefab has no Collider anywhere in its hierarchy. Add a BoxCollider to the Body child so the car collides with the ground.");
    }

    [Test]
    public void CarRigidbody_IsNotKinematic()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        var rb = prefab.GetComponent<Rigidbody>();
        Assume.That(rb, Is.Not.Null, "Skipping: Rigidbody missing (caught by CarPrefabHasRigidbody)");
        Assert.IsFalse(rb.isKinematic, "Car Rigidbody is set to Kinematic — physics forces won't move it. Uncheck 'Is Kinematic' in the Inspector.");
    }

    [Test]
    public void WheelPrefabHasCollider()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(WheelPrefabPath);
        Assert.IsNotNull(prefab, "Wheel prefab not found at " + WheelPrefabPath);

        var collider = prefab.GetComponentInChildren<Collider>();
        Assert.IsNotNull(collider, "Wheel prefab has no Collider. Add a CapsuleCollider to the Tyre child so the wheel rests on the ground correctly.");
    }
}
