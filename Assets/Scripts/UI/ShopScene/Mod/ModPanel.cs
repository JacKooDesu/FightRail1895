using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacDev.UI.ShopScene.Mod
{
    public class ModPanel : MonoBehaviour
    {
        public Entity.EntityEnums.EntityType modTarget;
        public List<Data.ModData> modTargetData;
        public Transform modBlank;
        public Transform modInventoryParent;
        public GameObject UIPrefab;

        public int currentSet = 0; // 配置

        List<ModUIObject> inventory = new List<ModUIObject>();
        List<ModUIObject> onBlank = new List<ModUIObject>();

        [Header("UI設定")]
        public Text modName;
        public Text modDescription;
        public Button[] setButtons;
        Color setTabOriginColor;

        private void Awake()
        {
            BindEvent();
        }

        private void OnEnable()
        {
            switch (modTarget)
            {
                case Entity.EntityEnums.EntityType.Train:
                    modTargetData = DataManager.Singleton.PlayerData.trainModDatas;
                    break;
                case Entity.EntityEnums.EntityType.Cabin:
                    modTargetData = DataManager.Singleton.PlayerData.cabinModDatas;
                    break;
                case Entity.EntityEnums.EntityType.Tower:
                    modTargetData = DataManager.Singleton.PlayerData.towerModDatas;
                    break;
            }

            BindInvertory();
            BindSet();

            ChangeSet(currentSet);
        }

        void BindSet()
        {
            onBlank = new List<ModUIObject>();

            // 綁定Setting Mod
            for (int i = 0; i < modBlank.childCount; ++i)
            {
                var modUI = modBlank.GetChild(i).GetComponent<ModUIObject>();

                modUI.isInstance = true;

                if (modTargetData.Find((x) => x.SetIndex[currentSet] == i) != null)
                {
                    modUI.Init(this, modTargetData.FindIndex((x) => x.SetIndex[currentSet] == i));
                }
                else
                {
                    modUI.Init(this, -1);
                }

                onBlank.Add(modUI);
            }
        }

        void BindInvertory()
        {
            foreach (Transform t in modInventoryParent)
                Destroy(t.gameObject);
            inventory = new List<ModUIObject>();

            var playerData = DataManager.Singleton.PlayerData;
            // 先綁定所有Inventory Mod
            for (int i = 0; i < playerData.modCapacity; ++i)
            {
                var modUI = Instantiate(UIPrefab, modInventoryParent).GetComponent<ModUIObject>();
                if (i < modTargetData.Count)
                {
                    modUI.Init(this, i);
                }
                else
                {
                    modUI.Init(this, -1);
                }

                inventory.Add(modUI);
            }
        }

        void BindEvent()
        {
            for (int i = 0; i < setButtons.Length; ++i)
            {
                var b = setButtons[i];
                int temp = i;
                b.onClick.AddListener(() => ChangeSet(temp));
            }

            setTabOriginColor = setButtons[0].image.color;
        }

        public void AddModToBlank(ModUIObject mod)
        {
            int iter = 0;
            if (modTargetData[mod.modIndex].SetIndex[currentSet] != -1)
                return;

            foreach (var blank in onBlank)
            {
                if (blank.modIndex != -1)
                {
                    iter++;
                    continue;
                }

                blank.Init(this, mod.modIndex);

                // data[mod.modIndex].bindingIndex = iter;
                modTargetData[mod.modIndex].SetIndex[currentSet] = iter;

                mod.Init(this, mod.modIndex);

                Audio.AudioHandler.Singleton.PlaySound("mod equip");

                DataManager.Singleton.SavePlayerData();
                break;
            }
        }

        public void RemoveModFromBlank(ModUIObject mod)
        {
            // data[mod.modIndex].bindingIndex = -1;
            modTargetData[mod.modIndex].SetIndex[currentSet] = -1;

            inventory[mod.modIndex].Init(this, mod.modIndex);

            mod.Init(this, -1);

            Audio.AudioHandler.Singleton.PlaySound("mod unequip");

            DataManager.Singleton.SavePlayerData();
        }

        public void ChangeSet(int i)
        {
            currentSet = i;
            BindInvertory();
            BindSet();

            for (int iter = 0; iter < setButtons.Length; ++iter)
            {
                var b = setButtons[iter];
                b.image.color = iter == currentSet ? setTabOriginColor : new Color(0, 0, 0, 0);
            }
        }
    }

}
