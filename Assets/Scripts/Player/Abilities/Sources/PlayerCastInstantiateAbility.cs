using UnityEngine;
using System.Collections;

public class PlayerCastInstantiateAbility : PlayerCastAbility
{
    public InstantiatedAbility instantiatedAbility;
    public float offset;

    protected override void OnCast(float castTime)
    {
        InstantiatedAbility instance = Instantiate(this.instantiatedAbility, this.transform.position + this.transform.forward * this.offset, this.transform.rotation);
        instance.OnInstantiated(this);
    }
}
