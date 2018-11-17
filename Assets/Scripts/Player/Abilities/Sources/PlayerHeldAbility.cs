using UnityEngine;
using System.Collections;

public class PlayerHeldAbility : PlayerAbility
{
    public float durationSeconds;
    protected float durationStartTime;

    public override void OnSuccessfulPress()
    {
        base.OnSuccessfulPress();
        this.enabled = true;

        this.StartCooldown();
        this.StartDuration();
    }

    public override void OnSuccessfulRelease()
    {
        base.OnSuccessfulRelease();
        this.enabled = false;
    }

    protected virtual void Update()
    {
        if (this.RemainingCooldownSeconds() < 0f)
        {
            this.OnSuccessfulRelease();
        }
    }

    protected void StartDuration()
    {
        this.durationStartTime = Time.time;
    }

    public float RemainingDuration()
    {
        return this.durationStartTime + this.durationSeconds - Time.time;
    }
}
