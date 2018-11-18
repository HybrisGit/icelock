using UnityEngine;
using System.Collections;

public class ImpulseOnCast : CastTimeScaledEffect, IInstantiateAbilityListener
{
    public Vector3 addedDirection;
    public float impulse;
    public float recoilEffect;

    protected override void Awake()
    {
        base.Awake();
        this.playerCastInstantiateAbility.RegisterInstantiateAbilityListener(this);
    }

    public void OnInstantiateAbility(InstantiatedAbility instantiatedAbility)
    {
        Rigidbody rigidbody = instantiatedAbility.GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            return;
        }
        
        Vector3 impulseVector = (this.transform.forward + this.addedDirection).normalized * this.impulse * this.GetCastTimeMultiplier();

        rigidbody.AddForce(impulseVector);

        if (this.recoilEffect != 0f)
        {
            this.playerCastInstantiateAbility.abilityManager.player.GetRigidbody().AddForce(impulseVector * - 1f * this.recoilEffect);
        }
    }
}
