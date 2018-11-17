using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    // references
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    public PlayerAbilityManager playerAbilityManager;
    public Health health;

    // variables
    public int playerNumber = 1;
    
    private new Rigidbody rigidbody;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    public Rigidbody GetRigidbody()
    {
        return this.rigidbody;
    }
}