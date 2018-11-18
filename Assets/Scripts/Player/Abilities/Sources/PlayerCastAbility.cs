using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCastAbility : PlayerAbility
{
    public float maxCastTime;
    protected float castStartTime;
    //public bool Casting
    //{
    //    get
    //    {
    //        return this._casting;
    //    }
    //    private set
    //    {
    //        if (this._casting == value)
    //        {
    //            return;
    //        }
    //        this._casting = value;
    //        this.enabled = this._casting;
    //    }
    //}
    //private bool _casting = false;
    public float CastTime
    {
        get
        {
            return this._castTime;
        }
        private set
        {
            if (this._castTime == value)
            {
                return;
            }
            this._castTime = Mathf.Max(value, 0f);
            Debug.Log("this._castTime " + this._castTime);
            this.castTimeListeners.ForEach((listener) => listener.OnRateChange(this, this._castTime / this.maxCastTime));
        }
    }
    private float _castTime = 0f;

    private List<IRateListener> castTimeListeners = new List<IRateListener>();

    protected override void Update()
    {
        base.Update();
        this.CastTime = Time.time - this.castStartTime;
    }

    public override void OnSuccessfulPress()
    {
        if (!this.abilityManager.TryStartCast(this))
        {
            return;
        }
        base.OnSuccessfulPress();
        this.CastTime = 0f;
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
        this.OnCast(this.CastTime);
    }

    protected virtual void OnCast(float castTime) { }

    public void RegisterCastTimeListener(IRateListener listener, bool updateImmediately = false)
    {
        this.castTimeListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnRateChange(this, this.CastTime / this.maxCastTime);
        }
    }

    public void DeregisterCastTimeListener(IRateListener listener)
    {
        this.castTimeListeners.Remove(listener);
    }
}
