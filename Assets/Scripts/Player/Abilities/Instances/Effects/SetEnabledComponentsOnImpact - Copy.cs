using UnityEngine;
using System.Collections;

public class SetEnabledComponentsOnImpact : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] components;
    [SerializeField] bool setEnabled;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (MonoBehaviour component in this.components)
        {
            component.enabled = this.setEnabled;
        }
    }
}
