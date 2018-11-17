using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ImpulseAbility : InstantiatedAbility
{
    public Vector3 addedDirection;
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

        Vector3 impulse = (this.transform.forward + this.addedDirection).normalized * this.impulse;

        this.rigidbody.AddForce(impulse);

        if (this.recoilEffect != 0f)
        {
            source.abilityManager.player.GetRigidbody().AddForce(impulse * -1f * this.recoilEffect);
        }
    }
}
