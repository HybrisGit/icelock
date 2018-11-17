using UnityEngine;
using System.Collections;

public class PlayerSelfAttachingAbility : MonoBehaviour
{
    public PlayerAbility ability;
    public PlayerAbilityManager manager;
    public int preferredIndex;

    private void Start()
    {
        this.manager.AddAbility(this.ability, this.preferredIndex);
    }
}
