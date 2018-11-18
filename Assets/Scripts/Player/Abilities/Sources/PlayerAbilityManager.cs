using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilityManager : MonoBehaviour, IRateListener
{
    public Player player;
    [SerializeField] int maxAbilities = 4;
    private PlayerAbility[] abilities;
    private PlayerCastAbility currentCastingAbility;
    private List<IEventListener> abilityChangeListeners = new List<IEventListener>();
    private List<IRateListener> castTimeListeners = new List<IRateListener>();

    private void Awake()
    {
        this.abilities = new PlayerAbility[this.maxAbilities];
    }

    private void Update()
    {
        if (this.player.playerHealth.Alive)
        {
            int number = this.player.playerNumber;
            for (int i = 0; i < this.abilities.Length; ++i)
            {
                this.HandleAbilityInput(number, i);
            }
        }
    }

    private void HandleAbilityInput(int playerNumber, int abilityIndex)
    {
        if (this.abilities[abilityIndex] == null)
        {
            return;
        }
        string inputButtonString = string.Format("P{0}_ABILITY_{1}", playerNumber, abilityIndex);
        string inputKeyString = string.Format("KEYBOARD_ABILITY_{0}", abilityIndex);

        if ((Input.GetButtonDown(inputButtonString) ||
            Input.GetButtonDown(inputKeyString)) &&
            !this.abilities[abilityIndex].Pressed)
        {
            if (!this.abilities[abilityIndex].OnCooldown)
            {
                this.abilities[abilityIndex].OnSuccessfulPress();
            }
        }

        if ((Input.GetButtonUp(inputButtonString) ||
            Input.GetButtonUp(inputKeyString)) &&
            this.abilities[abilityIndex].Pressed)
        {
            this.abilities[abilityIndex].OnSuccessfulRelease();
        }
    }

    public bool AddAbility(PlayerAbility ability, int startIndex = 0)
    {
        for (int i = 0; i < this.abilities.Length; ++i)
        {
            int currentIndex = (i + startIndex) % this.abilities.Length;
            if (this.abilities[currentIndex] == null)
            {
                this.abilities[currentIndex] = ability;
                ability.abilityManager = this;
                this.abilityChangeListeners.ForEach((listener) => listener.OnEvent());
                return true;
            }
        }
        return false;
    }

    public bool TryStartCast(PlayerCastAbility ability)
    {
        if (this.currentCastingAbility != null)
        {
            return false;
        }
        this.currentCastingAbility = ability;
        ability.RegisterCastTimeListener(this, true);
        this.castTimeListeners.ForEach((listener) => listener.OnRateChange(this, 0f));
        return true;
    }

    public bool TryEndCast(PlayerCastAbility ability)
    {
        if (this.currentCastingAbility != ability)
        {
            return false;
        }
        this.currentCastingAbility = null;
        ability.DeregisterCastTimeListener(this);
        this.castTimeListeners.ForEach((listener) => listener.OnRateChange(this, 0f));
        return true;
    }

    public List<PlayerAbility> GetAbilities()
    {
        List<PlayerAbility> abilities = new List<PlayerAbility>();
        for (int i = 0; i < this.abilities.Length; ++i)
        {
            if (this.abilities[i] != null)
            {
                abilities.Add(this.abilities[i]);
            }
        }
        return abilities;
    }

    public void RegisterAbilityListener(IEventListener listener, bool updateImmediately = false)
    {
        this.abilityChangeListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnEvent();
        }
    }

    public void DeregisterAbilityListener(IEventListener listener)
    {
        this.abilityChangeListeners.Remove(listener);
    }

    public void RegisterCastTimeListener(IRateListener listener, bool updateImmediately = false)
    {
        this.castTimeListeners.Add(listener);
        if (updateImmediately)
        {
            listener.OnRateChange(this, 0f);
        }
    }

    public void DeregisterCastTimeListener(IRateListener listener)
    {
        this.castTimeListeners.Remove(listener);
    }

    public void OnRateChange(object caller, float rate)
    {
        this.castTimeListeners.ForEach((listener) => listener.OnRateChange(this, rate));
    }
}
