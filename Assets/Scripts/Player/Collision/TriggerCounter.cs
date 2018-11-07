using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCounter : MonoBehaviour
{
    private List<Collider> currentTriggers = new List<Collider>();
    private List<IBinaryStateListener> anyTriggerListeners = new List<IBinaryStateListener>();

    private void OnTriggerEnter(Collider other)
    {
        if (!this.currentTriggers.Contains(other))
        {
            this.currentTriggers.Add(other);
            if (this.currentTriggers.Count == 1)
            {
                this.anyTriggerListeners.ForEach((listener) => listener.OnStateChange(this, true));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.currentTriggers.Remove(other);
        if (this.currentTriggers.Count == 0)
        {
            this.anyTriggerListeners.ForEach((listener) => listener.OnStateChange(this, false));
        }
    }

    public void RegisterAnyTriggerListener(IBinaryStateListener listener)
    {
        this.anyTriggerListeners.Add(listener);
    }
    public void DeregisterAnyTriggerListener(IBinaryStateListener listener)
    {
        this.anyTriggerListeners.Remove(listener);
    }

    public Collider[] GetCurrentTriggers()
    {
        return this.currentTriggers.ToArray();
    }

    public bool ContainsTriggers(Collider c)
    {
        return this.currentTriggers.Contains(c);
    }

    public bool ContainsAnyTriggers()
    {
        return this.currentTriggers.Count > 0;
    }

    public int GetTriggerCount()
    {
        return this.currentTriggers.Count;
    }
}
