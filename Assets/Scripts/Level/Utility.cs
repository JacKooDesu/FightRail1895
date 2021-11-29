using UnityEngine;

namespace JacDev.Level
{
    public static class Utility
    {
        public static float Normalize(float value, float valueMin, float valueMax, float resultMin, float resultMax)
        {
            if (valueMax - valueMin != 0f)
                return (value - valueMin) / (valueMax - valueMin) * (resultMax - resultMin) + resultMin;
            //return (value - valueMin) / (valueMax - valueMin) * (resultMax - resultMin) + resultMin;
            else
                return 0f;
        }

        public static void FlatShading(ref Vector3[] vertices, ref int[] triangles)
        {
            Vector3[] flatVertices = new Vector3[triangles.Length];

            for (int i = 0; i < triangles.Length; ++i)
            {
                flatVertices[i] = vertices[triangles[i]];

                triangles[i] = i;
            }

            vertices = flatVertices;
        }

        public static Mesh[] CreateMeshes(Vector3[] vertices, int[] triangles, int chunkSize)
        {
            Mesh[] meshes = new Mesh[Mathf.CeilToInt((float)vertices.Length / (float)chunkSize)];
            for (int i = 0; i < meshes.Length; ++i)
            {
                int elements = Mathf.Min(chunkSize, vertices.Length - i * chunkSize);   // ?
                Mesh mesh = new Mesh();
                Vector3[] meshVertices = new Vector3[elements];
                int[] meshTriangles = new int[elements];

                for (int j = 0; j < elements; ++j)
                {
                    meshVertices[j] = vertices[i * chunkSize + j];
                    meshTriangles[j] = triangles[i * chunkSize + j] % chunkSize;
                }

                mesh.vertices = meshVertices;
                mesh.triangles = meshTriangles;

                mesh.RecalculateNormals();
                meshes[i] = mesh;
            }

            return meshes;
        }
    }
}