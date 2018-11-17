using UnityEngine;
using System.Collections;

public class PlayerCastAbility : PlayerAbility
{
    protected float castStartTime;

    public override void OnSuccessfulPress()
    {
        this.castStartTime = Time.time;
    }

    public override void OnSuccessfulRelease()
    {
        this.StartCooldown();
        this.OnCast(this.CastTime());
    }

    protected virtual void OnCast(float castTime) { }

    public float CastTime()
    {
        return Time.time - this.castStartTime;
    }
}
