using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanDamage : MonoBehaviour, IBinaryStateListener
{
    [SerializeField] float damageTakenPerSecond;
    [SerializeField] SubmergedTrigger submergedTrigger;
    [SerializeField] Health health;

    private void Awake()
    {
        this.submergedTrigger.RegisterSubmergedListener(this, true);
    }

    private void Update()
    {
        this.health.TakeDamage(this.damageTakenPerSecond * Time.deltaTime);
    }

    public void OnStateChange(object caller, bool active)
    {
        this.enabled = active;
    }
}
