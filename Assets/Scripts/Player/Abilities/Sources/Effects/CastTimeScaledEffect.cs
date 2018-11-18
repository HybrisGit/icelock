using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCastInstantiateAbility))]
public class CastTimeScaledEffect : MonoBehaviour
{
    public bool scaleWithCastTime;
    public AnimationCurve castTimeScaling;
    protected PlayerCastInstantiateAbility playerCastInstantiateAbility;

    protected virtual void Awake()
    {
        this.playerCastInstantiateAbility = this.GetComponent<PlayerCastInstantiateAbility>();
    }

    protected float GetCastTimeMultiplier()
    {
        return this.scaleWithCastTime ? this.castTimeScaling.Evaluate(this.playerCastInstantiateAbility.CastTimeRelative) : 1f;
    }
}
