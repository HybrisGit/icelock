using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float oneOverMaxHealth;
    public float CurrentHealth
    {
        get
        {
            return this._currentHealth;
        }
        private set
        {
            if (this._currentHealth == value)
            {
                return;
            }
            this._currentHealth = value;
            this.Alive = this._currentHealth > 0f;
            this.healthListeners.ForEach((listener) => listener.OnRateChange(this, this._currentHealth * this.oneOverMaxHealth));
        }
    }
    private float _currentHealth;

    public bool Alive
    {
        get
        {
            return this._alive;
        }
        private set
        {
            if (this._alive == value)
            {
                return;
            }
            this._alive = value;
            this.stateListeners.ForEach((listener) => listener.OnStateChange(this, this._alive));
        }
    }
    private bool _alive;

    private List<IBinaryStateListener> stateListeners = new List<IBinaryStateListener>();
    private List<IRateListener> healthListeners = new List<IRateListener>();

    private void Awake()
    {
        this.oneOverMaxHealth = 1f / this.maxHealth;
        this.ResetDamage();
    }

    public void RegisterStateListener(IBinaryStateListener listener, bool updateImmediately = false)
    {
        this.stateListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnStateChange(this, this.Alive);
        }
    }
    public void DeregisterStateListener(IBinaryStateListener listener)
    {
        this.stateListeners.Remove(listener);
    }
    public void RegisterHealthListener(IRateListener listener, bool updateImmediately = false)
    {
        this.healthListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnRateChange(this, this.CurrentHealth);
        }
    }
    public void DeregisterHealthListener(IRateListener listener)
    {
        this.healthListeners.Remove(listener);
    }

    public void ResetDamage()
    {
        this.CurrentHealth = this.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        this.CurrentHealth -= damage;
    }
}
