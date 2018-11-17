using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilityManager : MonoBehaviour
{
    public Player player;
    [SerializeField] int maxAbilities = 4;
    private PlayerAbility[] abilities;

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
        //Debug.Log(string.Format("Ability Input, player {0}, ability {1}, input {2}, ability {3}", playerNumber, abilityIndex, Input.GetButton(inputButtonString), this.abilities[abilityIndex]));

        if ((Input.GetButtonDown(inputButtonString) ||
            Input.GetButtonDown(inputButtonString)) &&
            this.abilities[abilityIndex].RemainingCooldownSeconds() <= 0f)
        {
            this.abilities[abilityIndex].OnSuccessfulPress();
        }

        if (Input.GetButtonUp(inputButtonString) ||
            Input.GetButtonUp(inputKeyString))
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
                return true;
            }
        }
        return false;
    }
}
