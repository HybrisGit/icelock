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
    public Animator animationController;
    public float rotationDeadzone;
    public float movementDeadzone;
    public float rotationSpeed;
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
        if (this.player.playerHealth.Alive)
        {
            int number = this.player.playerNumber;
            float horizontalAxis = Input.GetAxis(string.Format("P{0}_HORIZONTAL", number)) + Input.GetAxis("KEYBOARD_HORIZONTAL");
            float verticalAxis = Input.GetAxis(string.Format("P{0}_VERTICAL", number)) + Input.GetAxis("KEYBOARD_VERTICAL");

            Vector3 inputVector = new Vector3(horizontalAxis, 0f, verticalAxis);
            float inputLength = inputVector.magnitude;
            if (inputLength > this.rotationDeadzone)
            {
                Vector3 inputDir = inputVector / inputLength;
                float movementLength = (inputLength - this.movementDeadzone) / (1f - this.movementDeadzone);
                if (movementLength > 0f)
                {
                    this.player.GetComponent<Rigidbody>().AddForce(inputDir * movementLength * this.movementForce);
                }

                float rotationLength = (inputLength - this.rotationDeadzone) / (1f - this.rotationDeadzone);
                Vector3 dir = Vector3.RotateTowards(this.player.transform.forward, inputDir * rotationLength, this.rotationSpeed * inputLength, 0f);

                this.player.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    public void SetPositionState(PositionState state)
    {
        if (this.positionState != state)
        {
            this.positionState = state;
            //Debug.Log("Set state to " + state);
            this.movementForce = this.stateSettings[state].force * this.player.GetComponent<Rigidbody>().mass;
            this.player.GetComponent<Rigidbody>().drag = this.stateSettings[state].drag;
        }
    }

    public StateSetting GetStateSetting(PositionState state)
    {
        return this.stateSettings[state];
    }
}
