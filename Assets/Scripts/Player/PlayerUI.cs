using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour, IRateListener, IEventListener
{
    [System.Serializable]
    public class ReferencedImage
    {
        public Image fillImage;
        [HideInInspector]
        public object callbackReference;
    }

    [SerializeField] ReferencedImage health;
    [SerializeField] ReferencedImage castTime;
    [SerializeField] List<ReferencedImage> abilityCooldowns;
    [SerializeField] float heightOffset;

    private Player player;

    private void Awake()
    {
        this.enabled = false;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        this.enabled = true;

        this.health.callbackReference = this.player.playerHealth;
        this.player.playerHealth.RegisterHealthListener(this, true);

        this.castTime.callbackReference = this.player.playerAbilityManager;
        this.player.playerAbilityManager.RegisterCastTimeListener(this, true);

        this.player.playerAbilityManager.RegisterAbilityListener(this, true);
    }

    private void LateUpdate()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(this.player.transform.position) + new Vector3(0f, this.heightOffset, 0f);
    }

    public void OnEvent()
    {
        List<PlayerAbility> playerAbilities = this.player.playerAbilityManager.GetAbilities();
        for (int i = 0; i < this.abilityCooldowns.Count; ++i)
        {
            if (i >= playerAbilities.Count)
            {
                this.abilityCooldowns[i].fillImage.fillAmount = 0f;
                continue;
            }
            if (this.abilityCooldowns[i].callbackReference != null)
            {
                (this.abilityCooldowns[i].callbackReference as PlayerAbility).DeregisterCooldownListener(this);
            }
            this.abilityCooldowns[i].callbackReference = playerAbilities[i];
            playerAbilities[i].RegisterCooldownListener(this, true);
        }
    }

    public void OnRateChange(object caller, float rate)
    {
        if (caller == this.health.callbackReference)
        {
            this.health.fillImage.fillAmount = rate;
            return;
        }

        if (caller == this.castTime.callbackReference)
        {
            this.castTime.fillImage.fillAmount = rate;
            return;
        }
        
        for (int i = 0; i < this.abilityCooldowns.Count; ++i)
        {
            if (caller == this.abilityCooldowns[i].callbackReference)
            {
                this.abilityCooldowns[i].fillImage.fillAmount = 1f - rate;
                return;
            }
        }
    }
}
