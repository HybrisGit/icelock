using UnityEngine;
using System.Collections;

public class AttachOnImpact : MonoBehaviour
{
    public bool Attached { get; private set; }
    public Rigidbody AttachedRigidbody { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        this.transform.SetParent(collision.collider.transform, true);
        this.transform.position = collision.contacts[0].point;

        Destroy(this.GetComponent<Rigidbody>());

        this.Attached = true;
        this.AttachedRigidbody = collision.rigidbody;
    }
}
