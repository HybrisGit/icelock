using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCastInstantiateAbility : PlayerCastAbility
{
    public InstantiatedAbility instantiatedAbility;
    private List<IInstantiateAbilityListener> instantiateAbilityListeners = new List<IInstantiateAbilityListener>();

    protected override void OnCast(float castTime)
    {
        InstantiatedAbility instance = Instantiate(this.instantiatedAbility, this.transform.position, this.transform.rotation);
        this.instantiateAbilityListeners.ForEach((listener) => listener.OnInstantiateAbility(instance, castTime));
        instance.OnInstantiated(this);
    }

    public void RegisterInstantiateAbilityListener(IInstantiateAbilityListener listener)
    {
        this.instantiateAbilityListeners.Add(listener);
    }

    public void DeregisterInstantiateAbilityListener(IInstantiateAbilityListener listener)
    {
        this.instantiateAbilityListeners.Remove(listener);
    }
}
