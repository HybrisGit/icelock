using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> trackedObjects;
    public Vector2 zoomRange;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float smoothing;

    private Vector3 targetPosition;

    private void Awake()
    {
        this.targetPosition = this.transform.position;
    }

    private void Update()
    {
        if (this.trackedObjects.Count > 0)
        {
            Bounds trackedBounds = new Bounds(this.trackedObjects[0].position, Vector3.zero);
            for (int i = 1; i < this.trackedObjects.Count; ++i)
            {
                if (this.trackedObjects[i] != null)
                {
                    trackedBounds.Encapsulate(this.trackedObjects[i].position);
                }
            }

            float xRatio = Mathf.Min(Mathf.Max(trackedBounds.size.x - this.minBounds.x, 0f) / (this.maxBounds.x - this.minBounds.x), 1f);
            float yRatio = Mathf.Min(Mathf.Max(trackedBounds.size.z - this.minBounds.y, 0f) / (this.maxBounds.y - this.minBounds.y), 1f);
            float maxRatio = Mathf.Max(xRatio, yRatio);
            float zoom = this.zoomRange.x + this.zoomRange.y * maxRatio;

            this.targetPosition = new Vector3(trackedBounds.center.x, 0f, trackedBounds.center.z);
            this.targetPosition = this.targetPosition + this.transform.forward * -1f * zoom;

            float smoothLerp = 1f;
            this.smoothing = Mathf.Max(this.smoothing, 0f);
            {
                if (this.smoothing < 1f)
                {
                    smoothLerp = 1f - Mathf.Pow(1f - this.smoothing, Time.deltaTime);
                }
            }
            this.transform.position = Vector3.Lerp(this.transform.position, this.targetPosition, smoothLerp);
        }
    }
}
