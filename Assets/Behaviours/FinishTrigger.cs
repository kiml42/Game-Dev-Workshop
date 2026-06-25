using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public RaceManager manager;

    private void OnTriggerEnter(Collider other)
    {
        manager.OnFinishCrossed(other.transform.root.gameObject);
    }
}
