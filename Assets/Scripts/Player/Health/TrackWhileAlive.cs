using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class TrackWhileAlive : MonoBehaviour, IBinaryStateListener
{
    private Health health;

    private void Awake()
    {
        this.health = this.GetComponent<Health>();
    }

    private void Start()
    {
        this.health.RegisterStateListener(this, true);
    }

    public void OnStateChange(object caller, bool active)
    {
        if (active)
        {
            CameraMovement.Instance.trackedObjects.Add(this.transform);
        }
        else
        {
            CameraMovement.Instance.trackedObjects.Remove(this.transform);
        }
    }
}
