using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IRateListener
{
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Health health;

    private GameObject healthBar;
    private Image healthBarFill;
    
    private void Start()
    {
        this.healthBar = Instantiate(this.healthBarPrefab, PlayerManager.Instance.healthBarParent);
        this.healthBarFill = this.healthBar.GetComponentsInChildren<Image>()[2];
        this.health.RegisterHealthListener(this, true);
    }

    private void LateUpdate()
    {
        this.healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    public void OnRateChange(object caller, float rate)
    {
        this.healthBarFill.fillAmount = rate;
    }
}
