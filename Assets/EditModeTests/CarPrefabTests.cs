using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CarPrefabTests
{
    private const string CarPrefabPath = "Assets/Prefabs/Car/Car.prefab";

    [Test]
    public void CarPrefabHasRigidbody()
    {
        var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(CarPrefabPath);
        Assert.IsNotNull(prefab, "Car prefab not found at " + CarPrefabPath);

        var rb = prefab.GetComponent<Rigidbody>();
        Assert.IsNotNull(rb, "Car prefab is missing a Rigidbody component. Add one in the Inspector so the car can move with physics.");
    }
}
