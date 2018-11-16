using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region MovementSpeed
    [Serializable]
    public struct StateSetting
    {
        public PositionState state;
        public float force;
        public float drag;
    }
    [SerializeField]
    private StateSetting[] editorStateSettings;
    private Dictionary<PositionState, StateSetting> stateSettings = new Dictionary<PositionState, StateSetting>();
    #endregion
    
    public float climbSpeed;
    private new Rigidbody rigidbody;
    private PositionState positionState;
    private float movementForce;

    public enum PositionState
    {
        Ground,
        Air,
        Water,
    }

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();

        foreach (StateSetting setting in this.editorStateSettings)
        {
            this.stateSettings.Add(setting.state, setting);
        }
        
        this.SetPositionState(PositionState.Air);
    }

    private void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("P1_HORIZONTAL") + Input.GetAxis("KEYBOARD_HORIZONTAL");
        float verticalAxis = Input.GetAxis("P1_VERTICAL") + Input.GetAxis("KEYBOARD_VERTICAL");

        Vector3 movementDirection = new Vector3(horizontalAxis, 0f, verticalAxis);
        float len = movementDirection.magnitude;
        if (len > 1.0f)
        {
            movementDirection /= len;
        }

        this.rigidbody.AddForce(movementDirection * this.movementForce);
    }

    public void SetPositionState(PositionState state)
    {
        if (this.positionState != state)
        {
            this.positionState = state;
            Debug.Log("Set state to " + state);
            this.movementForce = this.stateSettings[state].force * this.rigidbody.mass;
            this.rigidbody.drag = this.stateSettings[state].drag;
        }
    }

    public StateSetting GetStateSetting(PositionState state)
    {
        return this.stateSettings[state];
    }
}
