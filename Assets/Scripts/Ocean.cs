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
    
    public float density;
    [SerializeField]
    public WaveFunction waveFunction;

    //private void OnDrawGizmos()
    //{
    //    for (int x = 0; x < 100; ++x)
    //    {
    //        for (int y = 0; y < 100; ++y)
    //        {
    //            Vector3 pos = new Vector3(-0.5f + x * 0.01f, 0f, -0.5f + y * 0.01f);
    //            pos *= this.transform.localScale.x * 10f;
    //            pos.y = this.waveFunction.SurfaceHeight(pos);
    //            Gizmos.DrawCube(pos, Vector3.one * 0.5f);
    //        }
    //    }
    //}

    private void Update()
    {
        this.waveFunction.Set();
    }
}
