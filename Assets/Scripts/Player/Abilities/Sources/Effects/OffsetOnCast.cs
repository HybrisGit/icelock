using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCastInstantiateAbility))]
public class OffsetOnCast : MonoBehaviour, IInstantiateAbilityListener
{
    public Vector3 offset;
    private PlayerCastInstantiateAbility playerCastInstantiateAbility;

    private void Awake()
    {
        this.playerCastInstantiateAbility = this.GetComponent<PlayerCastInstantiateAbility>();
        this.playerCastInstantiateAbility.RegisterInstantiateAbilityListener(this);
    }

    public void OnInstantiateAbility(InstantiatedAbility instantiatedAbility)
    {
        instantiatedAbility.transform.position += this.transform.right * this.offset.x;
        instantiatedAbility.transform.position += this.transform.up * this.offset.y;
        instantiatedAbility.transform.position += this.transform.forward * this.offset.z;
    }
}
