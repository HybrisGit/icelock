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

    public Player player;
    public float climbSpeed;
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
        foreach (StateSetting setting in this.editorStateSettings)
        {
            this.stateSettings.Add(setting.state, setting);
        }
        
        this.SetPositionState(PositionState.Air);
    }

    private void FixedUpdate()
    {
        int number = this.player.playerNumber;
        float horizontalAxis = Input.GetAxis(string.Format("P{0}_HORIZONTAL", number)) + Input.GetAxis("KEYBOARD_HORIZONTAL");
        float verticalAxis = Input.GetAxis(string.Format("P{0}_VERTICAL", number)) + Input.GetAxis("KEYBOARD_VERTICAL");

        if (horizontalAxis != 0f || verticalAxis != 0f)
        {
            //Debug.Log(number + string.Format(" Input axes: {0} {1}", horizontalAxis, verticalAxis));
        }

        Vector3 movementDirection = new Vector3(horizontalAxis, 0f, verticalAxis);
        float len = movementDirection.magnitude;
        if (len > 1.0f)
        {
            movementDirection /= len;
        }

        this.player.rigidbody.AddForce(movementDirection * this.movementForce);
    }

    public void SetPositionState(PositionState state)
    {
        if (this.positionState != state)
        {
            this.positionState = state;
            Debug.Log("Set state to " + state);
            this.movementForce = this.stateSettings[state].force * this.player.rigidbody.mass;
            this.player.rigidbody.drag = this.stateSettings[state].drag;
        }
    }

    public StateSetting GetStateSetting(PositionState state)
    {
        return this.stateSettings[state];
    }
}
