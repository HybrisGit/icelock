using UnityEngine;
using System.Collections;

public class InstantiatedAbility : MonoBehaviour
{
    protected PlayerAbility source;

    public virtual void OnInstantiated(PlayerAbility source)
    {
        this.source = source;
    }
}
