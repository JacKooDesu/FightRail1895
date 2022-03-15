using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Data;

namespace JacDev.Mod
{
    public class ModObject : MonoBehaviour
    {
        public ModData modData;
        ModFactory modSetting;
        [SerializeField] Transform modObject;
        static Material[] modMatShared;

        [Header("預置物")]
        public GameObject cabin;
        public GameObject tower;
        public GameObject train;


        private void Start()
        {
            // BuildMod(new ModData(2, 2));
        }

        public void BuildMod(ModData data)
        {
            // 生成模型
            switch (data.ToModFactory().targetEntity)
            {
                case Entity.EntityEnums.EntityType.Tower:
                    modObject = Instantiate(tower, transform.GetChild(0)).transform;
                    break;

                case Entity.EntityEnums.EntityType.Cabin:
                    modObject = Instantiate(cabin, transform.GetChild(0)).transform;
                    break;

                case Entity.EntityEnums.EntityType.Train:
                    modObject = Instantiate(train, transform.GetChild(0)).transform;
                    break;

                default:
                    UnityEngine.Debug.LogWarning("unknow mod type");
                    return;
            }


            modData = data;
            modSetting = modData.ToModFactory();
            var qualityFactory = SettingManager.Singleton.ModList.qualityFactory;

            // 之後須注意把卡面放在 index = 0 的地方
            modMatShared = modObject.GetComponent<Renderer>().materials;

            // var originTexture = modMatShared.GetTexture("_MainTex");
            var qualityTextue = qualityFactory.GetQualitySetting(modData.ToModFactory()).GetQualityIcon(modData.rank).texture;
            var effectTexture = modSetting.icon.texture;

            var finalTexture = new Texture2D(qualityTextue.width, qualityTextue.height);

            finalTexture.SetPixels32(qualityTextue.GetPixels32());
            finalTexture.Apply();
            modMatShared[0].SetTexture("_MainTex", finalTexture);

            int startWidth = qualityTextue.width / 2 - effectTexture.width / 2;
            int startHeight = qualityTextue.height / 2 - effectTexture.height / 2;

            for (int y = 0; y < effectTexture.height; ++y)
            {
                for (int x = 0; x < effectTexture.width; ++x)
                {
                    Color pixel = effectTexture.GetPixel(x, y);
                    if (pixel.a == 0)
                        continue;
                    finalTexture.SetPixel(x + startWidth, y + startHeight, pixel);
                }
            }
            finalTexture.Apply();

            modMatShared[0].SetTexture("_MainTex", finalTexture);
            modObject.GetComponent<Renderer>().materials = modMatShared;
            //print(modObject.GetComponent<Renderer>().materials[0] == modMatShared);

            // 設定Outline
            var outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 4f;
        }

        [ContextMenu("更新外觀")]
        void Refresh()
        {
            BuildMod(modData);
        }
    }
}