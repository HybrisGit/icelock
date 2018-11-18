using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCastInstantiateAbility))]
public class VelocityOnCast : CastTimeScaledEffect, IInstantiateAbilityListener
{
    public Vector3 addedDirection;
    public float speed;

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
        
        rigidbody.velocity = (this.transform.forward + this.addedDirection).normalized * this.speed * this.GetCastTimeMultiplier();
    }
}
