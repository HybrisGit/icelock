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

    public int playerNumber;
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
        float horizontalAxis = Input.GetAxis(string.Format("P{0}_HORIZONTAL", this.playerNumber)) + Input.GetAxis("KEYBOARD_HORIZONTAL");
        float verticalAxis = Input.GetAxis(string.Format("P{0}_VERTICAL", this.playerNumber)) + Input.GetAxis("KEYBOARD_VERTICAL");

        if (horizontalAxis != 0f || verticalAxis != 0f)
        {
            Debug.Log(this.playerNumber + string.Format(" Input axes: {0} {1}", horizontalAxis, verticalAxis));
        }

        bool ability1 = Input.GetButtonDown(string.Format("P{0}_ABILITY_1", this.playerNumber));
        bool ability2 = Input.GetButtonDown(string.Format("P{0}_ABILITY_2", this.playerNumber));
        bool ability3 = Input.GetButtonDown(string.Format("P{0}_ABILITY_3", this.playerNumber));
        bool ability4 = Input.GetButtonDown(string.Format("P{0}_ABILITY_4", this.playerNumber));

        if (ability1 || ability2 || ability3 || ability4)
        {
            Debug.Log(this.playerNumber + " Input ability: " + (ability1 ? "1" : "") + (ability2 ? "2" : "") + (ability3 ? "3" : "") + (ability4 ? "4" : ""));
        }

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
