using UnityEngine;
using System.Collections;

public class SetActiveObjectsOnImpact : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] bool setActive;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject component in this.objects)
        {
            component.SetActive(this.setActive);
        }
    }
}
