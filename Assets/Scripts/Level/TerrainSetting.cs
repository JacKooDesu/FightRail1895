using UnityEngine;

namespace JacDev.Level
{
    [CreateAssetMenu(fileName = "TerraianSetting", menuName = "JacDev/Level/TerraianSetting", order = 0)]
    public class TerrainSetting : ScriptableObject
    {
        public int seed;
        public float scale;
        public int octaves;
        public float persistance;   // 高度持續
        public float lacunarity;    // 空缺
        public float falloffStrength;
        public float falloffRamp;
        public float falloffRange;
        public Vector2 offset;  // noise offset
        public float heightMultiplier;  // 高度
        public AnimationCurve heightCurve; // 山的長相
        public Gradient gradient;

        public int chunkSize = 6000;
    }
}
