using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float explosionRadius;

    public float maxDamage;
    public float maxImpulse;
    public AnimationCurve damageCurve;
    public AnimationCurve impulseCurve;

    private void Awake()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, this.explosionRadius, ~LayerMask.GetMask(LayerMask.LayerToName(this.gameObject.layer)), QueryTriggerInteraction.Ignore);
        foreach (Collider hit in hits)
        {
            Vector3 closestPoint = hit.ClosestPoint(this.transform.position);
            float distance = (this.transform.position - closestPoint).magnitude;
            float relativeDistance = distance / this.explosionRadius;

            Health health = hit.GetComponentInParent<Health>();
            if (health != null)
            {
                float damage = this.damageCurve.Evaluate(relativeDistance) * this.maxDamage;
                health.TakeDamage(damage);
                //Debug.Log("Dealing " + damage + " damage to " + health);
            }

            Rigidbody rigidbody = hit.GetComponentInParent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 impulse = this.impulseCurve.Evaluate(relativeDistance) * this.maxImpulse * (hit.transform.position - this.transform.position).normalized;
                rigidbody.AddForceAtPosition(impulse, closestPoint, ForceMode.Impulse);
                //Debug.Log("Applying impulse " + impulse + " to " + rigidbody);
            }
        }
    }
}
