using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergedTrigger : MonoBehaviour
{
    public Ocean ocean;
    public new Collider collider;
    
    private List<IBinaryStateListener> submergedListeners = new List<IBinaryStateListener>();
    private List<IRateListener> submersionRateListeners = new List<IRateListener>();
    public bool IsSubmerged
    {
        get
        {
            return this._isSubmerged;
        }
        private set
        {
            if (this._isSubmerged != value)
            {
                this._isSubmerged = value;
                this.submergedListeners.ForEach(listener => listener.OnStateChange(this, value));
            }
        }
    }
    private bool _isSubmerged = false;
    public float SubmersionRate
    {
        get
        {
            return this._submersionRate;
        }
        private set
        {
            float previousRate = this._submersionRate;
            if (value > 0.0f)
            {
                this._submersionRate = Mathf.Min(1.0f, value);
                this.IsSubmerged = true;
            }
            else
            {
                this._submersionRate = 0f;
                this.IsSubmerged = false;
            }
            if (this._submersionRate != previousRate)
            {
                this.submersionRateListeners.ForEach(listener => listener.OnRateChange(this, this._submersionRate));
            }
        }
    }
    private float _submersionRate = 0f;

    private void FixedUpdate()
    {
        this.SubmersionRate = Mathf.Clamp((this.ocean.waveFunction.SurfaceHeight(this.transform.position) - this.collider.bounds.min.y) / this.collider.bounds.size.y, 0f, 1f);
    }

    public void RegisterSubmergedListener(IBinaryStateListener listener)
    {
        this.submergedListeners.Add(listener);
    }
    public void DeregisterSubmergedListener(IBinaryStateListener listener)
    {
        this.submergedListeners.Remove(listener);
    }
}
