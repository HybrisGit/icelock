using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DestroyAtTime))]
public class SetDurationOnImpact : MonoBehaviour
{
    public float duration;

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<DestroyAtTime>().DestructionTime = Time.time + this.duration;
    }
}
