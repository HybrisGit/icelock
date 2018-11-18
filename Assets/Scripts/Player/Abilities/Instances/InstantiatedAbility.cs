using UnityEngine;
using System.Collections;

public class InstantiatedAbility : MonoBehaviour
{
    public PlayerAbility Source { get; private set; }

    public virtual void OnInstantiated(PlayerAbility source)
    {
        this.Source = source;
    }
}
