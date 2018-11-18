using UnityEngine;
using System.Collections;

public class DamageOnImpact : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.collider.GetComponentInParent<Health>();
        if (health)
        {
            health.TakeDamage(this.damage);
        }
    }
}
