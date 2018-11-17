using UnityEngine;
using System.Collections;

public class PlayerCastAbility : PlayerAbility
{
    protected float castStartTime;
    protected float castEndTime;
    protected bool casting = false;

    public override void OnSuccessfulPress()
    {
        base.OnSuccessfulPress();
        this.castStartTime = Time.time;
    }

    public override void OnSuccessfulRelease()
    {
        base.OnSuccessfulRelease();
        this.StartCooldown();
        this.castEndTime = Time.time;
        this.OnCast(this.CastTime());
    }

    protected virtual void OnCast(float castTime) { }

    public float CastTime()
    {
        return this.castEndTime - this.castStartTime;
    }
}
