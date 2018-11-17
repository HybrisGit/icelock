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
    private List<IRateListener> cooldownListeners = new List<IRateListener>();
    public bool Pressed { get; private set; }
    public float RemainingCooldown
    {
        get
        {
            return this._remainingCooldown;
        }
        private set
        {
            if (this.RemainingCooldown == value)
            {
                return;
            }
            this._remainingCooldown = Mathf.Max(value, 0f);
            this.cooldownListeners.ForEach((listener) => listener.OnRateChange(this, this.RemainingCooldown / this.cooldownSeconds));
        }
    }
    private float _remainingCooldown = 0f;

    private void Update()
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

    public bool OnCooldown()
    {
        return this.RemainingCooldown > 0f;
    }

    public bool OffCooldown()
    {
        return this.RemainingCooldown <= 0f;
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
