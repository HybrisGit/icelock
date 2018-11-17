using UnityEngine;
using System.Collections;

public abstract class PlayerAbility : MonoBehaviour
{
    public PlayerAbilityManager abilityManager;
    public float cooldownSeconds;
    private float cooldownStartTime = float.MinValue;

    public virtual void OnSuccessfulPress()
    {
        Debug.Log("Successful press " + this.name);
    }

    public virtual void OnSuccessfulRelease()
    {
        Debug.Log("Successful release " + this.name);
    }

    protected void StartCooldown()
    {
        this.cooldownStartTime = Time.time;
        Debug.Log("Cooldown started " + Time.time);
    }

    public float RemainingCooldownSeconds()
    {
        Debug.Log("Remaining cooldown is " + (this.cooldownStartTime + this.cooldownSeconds - Time.time));
        return this.cooldownStartTime + this.cooldownSeconds - Time.time;
    }
}
