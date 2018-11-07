using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour, IBinaryStateListener
{
    public new Rigidbody rigidbody;
    public SubmergedTrigger submergedTrigger;
    public float volume;
    
    void Awake ()
    {
        this.submergedTrigger.RegisterSubmergedListener(this);
        this.enabled = false;
    }

    void OnDestroy()
    {
        this.submergedTrigger.DeregisterSubmergedListener(this);
    }

    protected virtual void FixedUpdate()
    {
        this.rigidbody.AddForceAtPosition(Vector3.up * this.submergedTrigger.SubmersionRate * this.volume * this.submergedTrigger.ocean.density, this.submergedTrigger.transform.position);
    }

    public void OnStateChange(object caller, bool active)
    {
#pragma warning disable CS0252
        if (caller == this.submergedTrigger)
        {
            this.enabled = active;
        }
#pragma warning restore CS0252
    }
}
