using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ImpulseAbility : InstantiatedAbility
{
    public float impulse;
    public float recoilEffect;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    public override void OnInstantiated(PlayerAbility source)
    {
        base.OnInstantiated(source);

        this.rigidbody.AddForce(this.transform.forward * this.impulse);

        if (this.recoilEffect != 0f)
        {
            source.abilityManager.player.GetRigidbody().AddForce(this.transform.forward * this.impulse * -1f * this.recoilEffect);
        }
    }
}
