using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IBinaryStateListener
{
    public Player player;
    public TriggerCounter groundedTrigger;
    public SubmergedTrigger submergedTrigger;
    private PlayerMovement playerMovement;

    private bool isGrounded = false;
    private bool isSubmerged = false;

    private void Awake()
    {
        this.playerMovement = this.player.playerMovement;
    }

    void Start()
    {
        this.groundedTrigger.RegisterAnyTriggerListener(this);
        this.submergedTrigger.RegisterSubmergedListener(this);
    }

    void OnDestroy()
    {
        if (this.groundedTrigger != null)
        {
            this.groundedTrigger.DeregisterAnyTriggerListener(this);
        }
        if (this.submergedTrigger != null)
        {
            this.submergedTrigger.DeregisterSubmergedListener(this);
        }
    }

    public void OnStateChange(object caller, bool active)
    {
#pragma warning disable CS0252
        if (caller == this.groundedTrigger)
        {
            this.SetGrounded(active);
        }
        if (caller == this.submergedTrigger)
        {
            this.SetSubmerged(active);
        }
#pragma warning restore CS0252
    }

    private void SetGrounded(bool active)
    {
        this.isGrounded = active;
        this.UpdatePlayerMovementState();
    }

    private void SetSubmerged(bool active)
    {
        this.isSubmerged = active;
        this.UpdatePlayerMovementState();
    }

    private void UpdatePlayerMovementState()
    {
        if (this.isGrounded)
        {
            this.playerMovement.SetPositionState(PlayerMovement.PositionState.Ground);
        }
        else if (this.isSubmerged)
        {
            this.playerMovement.SetPositionState(PlayerMovement.PositionState.Water);
        }
        else
        {
            this.playerMovement.SetPositionState(PlayerMovement.PositionState.Air);
        }
    }
}
