using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IRateListener
{
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform healthBarParent;
    [SerializeField] Health health;

    private GameObject healthBar;
    private Image healthBarFill;
    
    private void Start()
    {
        this.healthBar = Instantiate(this.healthBarPrefab, this.healthBarParent);
        this.healthBarFill = this.healthBar.GetComponentsInChildren<Image>()[2];
        this.health.RegisterHealthListener(this, true);
    }

    private void Update()
    {
        this.healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    public void OnRateChange(object caller, float rate)
    {
        this.healthBarFill.fillAmount = rate;
    }
}
