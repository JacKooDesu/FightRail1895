using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Level
{
    [RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer)), ExecuteInEditMode]
    public class TerrainGenerator : MonoBehaviour
    {
        // public List<Vector3> vertices = new List<Vector3>();
        [HideInInspector] public Vertex[] vertices;
        [HideInInspector] public Vector3[] vertexDatas;
        [HideInInspector] public int[] triangles;

        [HideInInspector] public Vector2[] uvData;

        [HideInInspector] public Mesh[] meshes;
        [HideInInspector] public Color[] colorMap;

        [HideInInspector] public Material material;
        [HideInInspector] public Texture2D texture;

        [HideInInspector] public float[] heightMap;

        
        public Vector2Int size = new Vector2Int(500, 500); // 地形大小
        public Vector2Int resolution = new Vector2Int(100, 100);

        [HideInInspector] public Vector2 vertexDistance;

        public TerrainSetting setting;

        #region  private_variable
        [Header("細項")]
        [SerializeField] private int seed;
        [SerializeField] private float scale;
        [SerializeField] private int octaves;
        [SerializeField] private float persistance;   // 高度持續
        [SerializeField] private float lacunarity;    // 空缺
        [SerializeField] private float falloffStrength;
        [SerializeField] private float falloffRamp;
        [SerializeField] private float falloffRange;
        [SerializeField] private Vector2 offset;  // noise offset
        [SerializeField] private float heightMultiplier;  // 高度
        [SerializeField] private AnimationCurve heightCurve; // 山的長相
        [SerializeField] private Gradient gradient;

        [SerializeField] private int chunkSize = 6000;

        #endregion

        public void SetTerrainSetting(TerrainSetting setting)
        {
            this.setting = setting;

            BindSetting();
        }

        public void BindSetting()
        {
            if (setting == null) return;

            this.seed = setting.seed;
            this.scale = setting.scale;
            this.octaves = setting.octaves;
            this.persistance = setting.persistance;   // 高度持續
            this.lacunarity = setting.lacunarity;    // 空缺
            this.falloffStrength = setting.falloffStrength;
            this.falloffRamp = setting.falloffRamp;
            this.falloffRange = setting.falloffRange;
            this.offset = setting.offset;  // noise offset
            this.heightMultiplier = setting.heightMultiplier;  // 高度
            this.heightCurve = setting.heightCurve; // 山的長相
            this.gradient = setting.gradient;

            this.chunkSize = setting.chunkSize;
        }

        public void InitTerrain()
        {
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);

            heightMap = CreateHeightMap(seed, scale, octaves, persistance, lacunarity, falloffStrength, falloffRamp, falloffRange, offset, heightMultiplier, heightCurve);
            colorMap = CreateColorMap();

            vertices = new Vertex[resolution.x * resolution.y];
            vertexDatas = new Vector3[resolution.x * resolution.y];
            triangles = new int[6 * resolution.x * resolution.y];
            uvData = new Vector2[resolution.x * resolution.y];

            // 計算所有頂點距離
            vertexDistance = new Vector2(size.x / (float)resolution.x, size.y / (float)resolution.y);

            // Create vertices
            for (int y = 0; y < resolution.y; ++y)
            {
                for (int x = 0; x < resolution.x; ++x)
                {
                    int index = GridToArray(x, y);
                    Vector2 position = GridToWorld(x, y);
                    vertices[index] = new Vertex(index);
                    vertexDatas[index] = new Vector3(position.x, heightMap[index], position.y);    // 後續須製作Height Map
                }
            }

            // Create triangles
            int triIndex = 0;
            for (int y = 0; y < resolution.y - 1; ++y)
            {
                for (int x = 0; x < resolution.x - 1; ++x)
                {
                    // b  d
                    // .__.
                    // | /|
                    // .__.
                    // a  c
                    int a = GridToArray(x + 0, y + 0);
                    int b = GridToArray(x + 0, y + 1);
                    int c = GridToArray(x + 1, y + 0);
                    int d = GridToArray(x + 1, y + 1);

                    //----------------------------------------------------------------------------//
                    triangles[triIndex] = a;
                    vertices[a].AddVertexIndex(triIndex);
                    vertices[a].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;

                    triangles[triIndex] = d;
                    vertices[d].AddVertexIndex(triIndex);
                    vertices[d].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;

                    triangles[triIndex] = c;
                    vertices[c].AddVertexIndex(triIndex);
                    vertices[c].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;
                    //----------------------------------------------------------------------------//
                    triangles[triIndex] = d;
                    vertices[d].AddVertexIndex(triIndex);
                    vertices[d].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;

                    triangles[triIndex] = a;
                    vertices[a].AddVertexIndex(triIndex);
                    vertices[a].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;

                    triangles[triIndex] = b;
                    vertices[b].AddVertexIndex(triIndex);
                    vertices[b].AddMeshIndex(Mathf.FloorToInt((float)triIndex / (float)chunkSize));
                    triIndex++;
                    //----------------------------------------------------------------------------//
                }
            }

            // Create UV
            for (int y = 0; y < resolution.y; ++y)
            {
                for (int x = 0; x < resolution.x; ++x)
                {
                    float percentX = (float)x / (float)resolution.x;
                    float percentY = (float)y / (float)resolution.y;
                    uvData[GridToArray(x, y)] = new Vector2(percentX, percentY);
                }
            }

            // Apply flat shading
            Utility.FlatShading(ref vertexDatas, ref triangles, ref uvData);

            // Create Meshes
            meshes = Utility.CreateMeshes(vertexDatas, triangles, uvData, chunkSize);

            // Create material
            material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            texture = Utility.CreateTexture(colorMap, resolution.x, resolution.y, FilterMode.Trilinear);
            material.mainTexture = texture;

            // Set material smoothness
            material.SetFloat("_Smoothness", 0f);

            // Instantiate
            for (int i = 0; i < meshes.Length; ++i)
            {
                GameObject g = new GameObject($"Mesh {i}");
                g.transform.SetParent(transform);
                MeshRenderer renderer = g.AddComponent<MeshRenderer>();
                renderer.sharedMaterial = material;
                MeshFilter filter = g.AddComponent<MeshFilter>();
                filter.sharedMesh = meshes[i];
                g.AddComponent<MeshCollider>();
            }
        }

        public Color[] CreateColorMap()
        {
            Color[] colorMap = new Color[resolution.x * resolution.y];
            for (int y = 0; y < resolution.y; ++y)
            {
                for (int x = 0; x < resolution.x; ++x)
                {
                    float height = vertices[GridToArray(x, y)].GetHeight(heightMap) / heightMultiplier;
                    // print(height);
                    Color c = gradient.Evaluate(height);
                    colorMap[GridToArray(x, y)] = c;
                }
            }

            return colorMap;
        }

        public float[] CreateHeightMap(
            int seed, float scale, int octaves,
            float persistance, float lacunarity,
            float falloffStrength, float falloffRamp, float falloffRange,
            Vector2 offset, float heightMultiplier, AnimationCurve heightCurve)
        {
            float[] heightMap = new float[resolution.x * resolution.y];

            Vector2[] octaveOffset = new Vector2[octaves];
            Random.InitState(seed);

            for (int i = 0; i < octaves; ++i)
            {
                float offsetX = Random.Range(-100f, 100f);
                float offsetY = Random.Range(-100f, 100f);
                octaveOffset[i] = new Vector2(offsetX, offsetY);
            }

            float maxLocalNoiseHeight = float.MinValue;
            float minLocalNoiseHeight = float.MaxValue;
            for (int y = 0; y < resolution.y; ++y)
            {
                for (int x = 0; x < resolution.x; ++x)
                {
                    float amplitude = 1f;   // 震幅
                    float freq = 1f;    // 頻率
                    float noiseHeight = 0;

                    for (int i = 0; i < octaves; ++i)
                    {
                        float xPos = (((float)x + offset.x) - (float)resolution.x / 2f) / (float)resolution.x;
                        float yPos = (((float)y + offset.y) - (float)resolution.y / 2f) / (float)resolution.y;
                        float sampleX = freq * scale * xPos + octaveOffset[i].x;    // ?
                        float sampleY = freq * scale * yPos + octaveOffset[i].y;

                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2f - 1f;  // why?
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= persistance;   // 高度
                        freq *= lacunarity;     // 空缺
                    }

                    // Get min & max
                    maxLocalNoiseHeight = Mathf.Max(maxLocalNoiseHeight, noiseHeight);
                    minLocalNoiseHeight = Mathf.Min(minLocalNoiseHeight, noiseHeight);

                    heightMap[GridToArray(x, y)] = noiseHeight;
                }
            }

            for (int y = 0; y < resolution.y; ++y)
            {
                for (int x = 0; x < resolution.x; ++x)
                {
                    float value = Mathf.Max(Mathf.Abs(x / (float)resolution.x * 2f - 1f), Mathf.Abs(y / (float)resolution.y * 2f - 1f));
                    float a = Mathf.Pow(value, falloffRamp);
                    float b = Mathf.Pow(falloffRange - falloffRange * value, falloffRamp);
                    float falloff = 1f - (a + b != 0f ? falloffStrength * a / (a + b) : 0f);
                    // print(Utility.Normalize(heightMap[GridToArray(x, y)], minLocalNoiseHeight, maxLocalNoiseHeight, 0f, 1f));
                    heightMap[GridToArray(x, y)] = heightMultiplier * heightCurve.Evaluate(falloff * Utility.Normalize(heightMap[GridToArray(x, y)], minLocalNoiseHeight, maxLocalNoiseHeight, 0f, 1f));
                    //heightMap[GridToArray(x, y)] = heightMultiplier * heightCurve.Evaluate(Mathf.Clamp01(heightMap[GridToArray(x, y)]));

                }
            }

            return heightMap;
        }

        void Update()
        {

        }

        // private void OnDrawGizmos()
        // {
        //     if (vertices.Count == 0)
        //         return;

        //     foreach (Vector3 v3 in vertices)
        //     {
        //         Gizmos.color = Color.red;
        //         Gizmos.DrawSphere(v3, .1f);
        //     }
        // }

        public int GridToArray(int x, int y)
        {
            return y * resolution.x + x;
        }

        public Vector2 GridToWorld(int x, int y)
        {
            return new Vector2(
                size.x * (float)x / ((float)resolution.x - 1f) - size.x / 2f,
                size.y * (float)y / ((float)resolution.y - 1f) - size.y / 2f);
        }

        private void OnEnable()
        {
            InitTerrain();
        }
    }

    [System.Serializable]
    public class Vertex
    {
        public int index;
        public int[] vertexIndices = new int[0];
        public int[] meshIndices = new int[0];

        public Vertex(int index)
        {
            this.index = index;
        }

        public void AddVertexIndex(int index)
        {
            System.Array.Resize(ref vertexIndices, vertexIndices.Length + 1);
            vertexIndices[vertexIndices.Length - 1] = index;
        }

        public void AddMeshIndex(int index)
        {
            for (int i = 0; i < meshIndices.Length; ++i)
            {
                if (meshIndices[i] == index)
                {
                    return;
                }
            }

            System.Array.Resize(ref meshIndices, meshIndices.Length + 1);
            meshIndices[meshIndices.Length - 1] = index;
        }

        public float GetHeight(float[] heightMap)
        {
            return heightMap[index];
        }
    }
}
