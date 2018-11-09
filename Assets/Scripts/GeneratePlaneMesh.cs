using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlaneMesh : MonoBehaviour
{
    public MeshFilter meshFilter;
    public float size;
    public float vertexDensity;

    private void Awake()
    {
        int quadsPerDimension = Mathf.CeilToInt(this.size * this.vertexDensity);
        int verticesPerDimension = quadsPerDimension + 1;
        int quadsTotal = quadsPerDimension * quadsPerDimension;
        Vector3[] vertices = new Vector3[verticesPerDimension * verticesPerDimension];
        int[] triangles = new int[quadsTotal * 6];
        float vertDist = this.size / quadsPerDimension;

        // create vertices
        for (int i = 0; i < verticesPerDimension; ++i)
        {
            for (int j = 0; j < verticesPerDimension; ++j)
            {
                vertices[i * verticesPerDimension + j] = 
                    new Vector3(i * vertDist - 0.5f * this.size, 0f, j * vertDist - 0.5f * this.size);
            }
        }

        // create triangles
        int nQuad = 0;
        for (int i = 0; i < quadsPerDimension; ++i)
        {
            for (int j = 0; j < quadsPerDimension; ++j)
            {
                // top-left quad (origo in top-left)
                triangles[nQuad * 6 + 0] = (i + 0) * verticesPerDimension + (j + 0);
                triangles[nQuad * 6 + 1] = (i + 0) * verticesPerDimension + (j + 1);
                triangles[nQuad * 6 + 2] = (i + 1) * verticesPerDimension + (j + 0);

                // top-left quad (origo in top-left)
                triangles[nQuad * 6 + 3] = (i + 0) * verticesPerDimension + (j + 1);
                triangles[nQuad * 6 + 4] = (i + 1) * verticesPerDimension + (j + 1);
                triangles[nQuad * 6 + 5] = (i + 1) * verticesPerDimension + (j + 0);

                nQuad++;
            }
        }

        // set mesh
        this.meshFilter.mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles
        };
    }
}
