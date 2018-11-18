using UnityEngine;
using System.Collections;

public class DestroyComponentsOnImpact : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] components;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (MonoBehaviour component in this.components)
        {
            Destroy(component);
        }
    }
}
