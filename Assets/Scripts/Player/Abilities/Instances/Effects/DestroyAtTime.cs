using UnityEngine;
using System.Collections;

public class DestroyAtTime : MonoBehaviour
{
    [SerializeField] float initialTimeOffset;
    public float DestructionTime { get; set; }

    private void Awake()
    {
        this.DestructionTime = Time.time + this.initialTimeOffset;
    }

    private void Update()
    {
        if (Time.time > this.DestructionTime)
        {
            Destroy(this.gameObject);
        }
    }
}
