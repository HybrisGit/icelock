using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCastInstantiateAbility))]
public class ImpulseOnCast : MonoBehaviour, IInstantiateAbilityListener
{
    public Vector3 addedDirection;
    public float impulse;
    public float recoilEffect;
    public bool scaleWithCastTime;
    public float maxEffectiveCastTime;
    public AnimationCurve castTimeImpulseScaling;
    private PlayerCastInstantiateAbility playerCastInstantiateAbility;

    private void Awake()
    {
        this.playerCastInstantiateAbility = this.GetComponent<PlayerCastInstantiateAbility>();
        this.playerCastInstantiateAbility.RegisterInstantiateAbilityListener(this);
    }

    public void OnInstantiateAbility(InstantiatedAbility instantiatedAbility, float castTime)
    {
        Rigidbody rigidbody = instantiatedAbility.GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            return;
        }

        float castTimeMult = 1f;
        if (this.scaleWithCastTime)
        {
            castTimeMult = this.castTimeImpulseScaling.Evaluate(Mathf.Max(castTime / this.maxEffectiveCastTime, 0f));
        }
        
        Vector3 impulseVector = (this.transform.forward + this.addedDirection).normalized * this.impulse * castTimeMult;

        rigidbody.AddForce(impulseVector);

        if (this.recoilEffect != 0f)
        {
            this.playerCastInstantiateAbility.abilityManager.player.GetRigidbody().AddForce(impulseVector * - 1f * this.recoilEffect);
        }
    }
}
