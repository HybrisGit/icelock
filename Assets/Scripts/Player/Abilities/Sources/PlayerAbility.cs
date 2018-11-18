using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlayerAbility : MonoBehaviour
{
    public PlayerAbilityManager abilityManager;
    public float cooldownSeconds;
    private float CooldownStartTime
    {
        get
        {
            return this._cooldownStartTime;
        }
        set
        {
            this._cooldownStartTime = value;
            this.RemainingCooldown = this.CooldownStartTime + this.cooldownSeconds - Time.time;
        }
    }
    private float _cooldownStartTime = float.MinValue;
    public bool Pressed
    {
        get
        {
            return this._pressed;
        }
        private set
        {
            if (this._pressed == value)
            {
                return;
            }
            this._pressed = value;
        }
    }
    private bool _pressed = false;
    public bool OnCooldown { get; private set; }
    public float RemainingCooldown
    {
        get
        {
            return this._remainingCooldown;
        }
        private set
        {
            if (this._remainingCooldown == value)
            {
                return;
            }
            if (value <= 0f)
            {
                this._remainingCooldown = 0f;
                this.OnCooldown = false;
            }
            else
            {
                this._remainingCooldown = value;
                this.OnCooldown = true;
            }
            this.cooldownListeners.ForEach((listener) => listener.OnRateChange(this, this._remainingCooldown / this.cooldownSeconds));
        }
    }
    private float _remainingCooldown = 0f;

    private List<IRateListener> cooldownListeners = new List<IRateListener>();

    protected virtual void Update()
    {
        this.RemainingCooldown = this.CooldownStartTime + this.cooldownSeconds - Time.time;
    }

    public virtual void OnSuccessfulPress()
    {
        this.Pressed = true;
    }

    public virtual void OnSuccessfulRelease()
    {
        this.Pressed = false;
    }

    protected void StartCooldown()
    {
        this.CooldownStartTime = Time.time;
    }

    public void RegisterCooldownListener(IRateListener listener, bool updateImmediately = false)
    {
        this.cooldownListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnRateChange(this, this.RemainingCooldown / this.cooldownSeconds);
        }
    }

    public void DeregisterCooldownListener(IRateListener listener)
    {
        this.cooldownListeners.Remove(listener);
    }
}
