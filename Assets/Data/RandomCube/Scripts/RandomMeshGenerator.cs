using UnityEngine;

namespace Data.RandomCube.Scripts
{
    public static class RandomMeshGenerator 
    {
        private const float MinOffset = -0.1f;
        private const float MaxOffset = 0.1f;

        public static MeshFilter CreateCubeRandomMesh(float cubeSize , MeshFilter meshFilter)
        {
            Mesh mesh = GenerateCubeMesh();

            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(MinOffset, MaxOffset),
                    Random.Range(MinOffset, MaxOffset),
                    Random.Range(MinOffset, MaxOffset)
                );
                vertices[i] = (vertices[i] + offset).normalized * (cubeSize / 2f);
            }
            mesh.vertices = vertices;
            
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            
            meshFilter.mesh = mesh;
            
            return meshFilter;
        }
        
        private static Mesh GenerateCubeMesh()
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f)
            };

            int[] triangles = new int[]
            {
                0, 2, 1,
                0, 3, 2,
                2, 3, 4,
                2, 4, 5,
                1, 2, 5,
                1, 5, 6,
                0, 7, 4,
                0, 4, 3,
                1, 6, 7,
                1, 7, 0,
                5, 4, 7,
                5, 7, 6
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            return mesh;
        }
    }
}
