using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delaySeconds;

    private void Awake()
    {
        Destroy(this.gameObject, this.delaySeconds);
    }
}
