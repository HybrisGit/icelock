using UnityEngine;
using System.Collections;

public class InstantiateOnImpact : MonoBehaviour
{
    public GameObject prefab;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hit = Vector3.zero;
        foreach (ContactPoint point in collision.contacts)
        {
            hit += point.point;
        }
        hit /= collision.contacts.Length;

        Debug.Log("Impact at " + hit + ", instantiating " + prefab);
        GameObject instance = Instantiate(this.prefab, hit, this.transform.rotation);
    }
}
