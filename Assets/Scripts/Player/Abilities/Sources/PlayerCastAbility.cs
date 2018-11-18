using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCastAbility : PlayerAbility
{
    public float maxCastTime;
    protected float castStartTime;
    public float CastTimeAbsolute
    {
        get
        {
            return this._castTime;
        }
        private set
        {
            value = Mathf.Clamp(value, 0f, this.maxCastTime);
            if (this._castTime == value)
            {
                return;
            }
            this._castTime = value;
            this.castTimeListeners.ForEach((listener) => listener.OnRateChange(this, this.CastTimeRelative));
        }
    }
    public float CastTimeRelative
    {
        get
        {
            return this.CastTimeAbsolute / this.maxCastTime;
        }
    }
    private float _castTime = 0f;

    private List<IRateListener> castTimeListeners = new List<IRateListener>();

    protected override void Update()
    {
        base.Update();
        if (this.Pressed)
        {
            this.CastTimeAbsolute = Time.time - this.castStartTime;
        }
    }

    public override void OnSuccessfulPress()
    {
        if (!this.abilityManager.TryStartCast(this))
        {
            return;
        }
        base.OnSuccessfulPress();
        this.CastTimeAbsolute = 0f;
        this.castStartTime = Time.time;
    }

    public override void OnSuccessfulRelease()
    {
        if (!this.abilityManager.TryEndCast(this))
        {
            return;
        }
        base.OnSuccessfulRelease();
        this.StartCooldown();
        this.OnCast();
    }

    protected virtual void OnCast() { }

    public void RegisterCastTimeListener(IRateListener listener, bool updateImmediately = false)
    {
        this.castTimeListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnRateChange(this, this.CastTimeAbsolute / this.maxCastTime);
        }
    }

    public void DeregisterCastTimeListener(IRateListener listener)
    {
        this.castTimeListeners.Remove(listener);
    }
}
