using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPosition : MonoBehaviour
{
    public Transform center;
    public float radius;
    public float angle;

    private void Update()
    {
        this.transform.localPosition = new Vector3(Mathf.Cos(this.angle * Mathf.Deg2Rad), 0f, Mathf.Sin(this.angle * Mathf.Deg2Rad)) * this.radius;
    }
}
