using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergedClimb : MonoBehaviour, IBinaryStateListener
{
    public new Rigidbody rigidbody;
    public SubmergedTrigger submergedTrigger;
    public TriggerCounter climbTrigger;
    private bool isClimbing = false;

    void Awake()
    {
        this.submergedTrigger.RegisterSubmergedListener(this);
        this.climbTrigger.RegisterAnyTriggerListener(this);
        this.enabled = false;
    }

    void OnDestroy()
    {
        this.submergedTrigger.DeregisterSubmergedListener(this);
        this.climbTrigger.DeregisterAnyTriggerListener(this);
    }

    void FixedUpdate()
    {
        if (this.isClimbing)
        {
            this.rigidbody.AddForce(-1f * Physics.gravity * (this.submergedTrigger.SubmersionRate + 1.0f) * this.rigidbody.mass);
        }
    }

    public void OnStateChange(object caller, bool active)
    {
#pragma warning disable CS0252
        if (caller == this.submergedTrigger)
        {
            this.enabled = active;
        }
        if (caller == this.climbTrigger)
        {
            this.isClimbing = active;
        }
#pragma warning restore CS0252
    }
}
