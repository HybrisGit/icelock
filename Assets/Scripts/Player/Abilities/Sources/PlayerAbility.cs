using UnityEngine;
using System.Collections;

public abstract class PlayerAbility : MonoBehaviour
{
    public PlayerAbilityManager abilityManager;
    public float cooldownSeconds;
    private float cooldownStartTime = float.MinValue;

    public abstract void OnSuccessfulPress();

    public abstract void OnSuccessfulRelease();

    protected void StartCooldown()
    {
        this.cooldownStartTime = Time.time;
    }

    public float RemainingCooldownSeconds()
    {
        return this.cooldownStartTime + this.cooldownSeconds - Time.time;
    }
}
