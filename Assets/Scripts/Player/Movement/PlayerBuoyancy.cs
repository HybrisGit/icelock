using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuoyancy : Buoyancy
{
    public PlayerMovement playerMovement;
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.rigidbody.drag = this.playerMovement.GetStateSetting(PlayerMovement.PositionState.Water).drag * this.submergedTrigger.SubmersionRate + 
            this.playerMovement.GetStateSetting(PlayerMovement.PositionState.Air).drag * (1f - this.submergedTrigger.SubmersionRate);
    }
}
