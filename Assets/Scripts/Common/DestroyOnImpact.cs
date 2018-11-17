using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
