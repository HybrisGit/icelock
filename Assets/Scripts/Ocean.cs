using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    [System.Serializable]
    public class WaveFunction
    {
        public Renderer renderer;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float amplitude;
        [SerializeField]
        private float length;
        [SerializeField]
        private float angle;
        private float period;
        private Vector2 dir;

        private float TFromWorldPos(Vector2 pos)
        {
            return Vector2.Dot(this.dir, pos);
        }

        public float SurfaceHeight(Vector2 pos)
        {
            return this.amplitude * Mathf.Sin(this.period * this.TFromWorldPos(pos) + this.speed * Time.time);
        }
        public float SurfaceHeight(Vector3 pos)
        {
            return this.SurfaceHeight(new Vector2(pos.x, pos.z));
        }

        public void Set()
        {
            this.SetAngle(this.angle);
            this.SetLength(this.length);

            this.renderer.material.SetFloat("_Amplitude", this.amplitude);
            this.renderer.material.SetFloat("_Speed", this.speed);
        }

        public float GetAngle()
        {
            return this.angle;
        }
        public float GetLength()
        {
            return this.length;
        }
        public Vector2 GetDirection()
        {
            return this.dir;
        }
        public void SetAngle(float angle)
        {
            this.angle = angle;
            this.dir = new Vector2(Mathf.Cos(this.angle), Mathf.Sin(this.angle));
            this.renderer.material.SetVector("_Direction", this.dir);
        }
        public void SetLength(float length)
        {
            this.length = length;
            this.period = 1f / this.length;
            this.renderer.material.SetFloat("_Period", this.period);
        }
    }
    
    public static Ocean Instance { get; private set; }
    public float density;
    [SerializeField]
    public WaveFunction waveFunction;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, 10f * new Vector3(this.waveFunction.GetDirection().x, 0f, this.waveFunction.GetDirection().y));
    }

    private void Update()
    {
        this.waveFunction.Set();
    }
}
