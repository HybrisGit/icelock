using UnityEngine;
using System.Collections;

public interface IInstantiateAbilityListener
{
    void OnInstantiateAbility(InstantiatedAbility instantiatedAbility, float castTime);
}
