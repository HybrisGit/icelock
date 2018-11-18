using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AttachOnImpact), typeof(InstantiatedAbility), typeof(LineRenderer))]
public class PullToAttachment : MonoBehaviour
{
    [SerializeField] float pullForce;
    [SerializeField] float casterPullShare;
    [SerializeField] float destroyDistance;
    private Rigidbody casterRigidbody;
    private Rigidbody attachedRigidbody;

    private AttachOnImpact attachOnImpact;
    private InstantiatedAbility instantiatedAbility;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        this.attachOnImpact = this.GetComponent<AttachOnImpact>();
        this.instantiatedAbility = this.GetComponent<InstantiatedAbility>();
        this.lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (this.instantiatedAbility.Source != null)
        {
            this.lineRenderer.SetPositions(new Vector3[] { this.transform.position, this.instantiatedAbility.Source.transform.position });
        }
    }

    private void FixedUpdate()
    {
        if (this.casterRigidbody == null)
        {
            if (this.instantiatedAbility.Source != null)
            {
                this.casterRigidbody = this.instantiatedAbility.Source.abilityManager.player.GetRigidbody();
            }
            else
            {
                return;
            }
        }
        if (this.attachedRigidbody == null)
        {
            if (this.attachOnImpact.AttachedRigidbody != null)
            {
                this.attachedRigidbody = this.attachOnImpact.AttachedRigidbody;
                this.lineRenderer.SetColors(Color.black, Color.black);
            }
            else
            {
                return;
            }
        }

        Vector3 casterToAttachment = this.transform.position - this.casterRigidbody.transform.position;
        float distance = casterToAttachment.magnitude;
        if (distance < this.destroyDistance)
        {
            Destroy(this.gameObject);
            return; 
        }
        Vector3 casterToAttachmentDir = casterToAttachment / distance;
        
        this.casterRigidbody.AddForce(casterToAttachmentDir * this.pullForce * this.casterPullShare, ForceMode.Force);
        this.attachedRigidbody.AddForce(casterToAttachmentDir * -1f * this.pullForce * (1f - this.casterPullShare), ForceMode.Force);
    }
}
