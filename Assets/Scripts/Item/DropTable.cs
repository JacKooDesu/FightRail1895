using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Mod;
using JacDev.Entity;

namespace JacDev.Item
{
    [CreateAssetMenu(fileName = "Drop Table", menuName = "JacDev/Item/Create Drop Table", order = 0)]
    public class DropTable : ScriptableObject
    {
        [Header("總掉落機率"), Tooltip("需大於此掉落率，才計算最後掉落的物品")]
        public AnimationCurve dropRate; // 需大於此掉落率，才計算最後掉落的物品

        [System.Serializable]
        public class DropSettingBase
        {
            [HideInInspector] public string name;

            [Header("機率")]
            public AnimationCurve dropRate;
        }

        [System.Serializable]
        public class ModDropSetting : DropSettingBase
        {
            [Header("掉落物")]
            public ModFactory mod;
        }

        [System.Serializable]
        public class ItemDropSetting : DropSettingBase
        {
            [Header("掉落物")]
            public DropItem item;
            [Header("數量設定")]
            public int minAmount;
            public int maxAmount;
        }

        [Header("MOD掉落率")]
        public List<ModDropSetting> modDropSettings = new List<ModDropSetting>();
        [Header("物品掉落率")]
        public List<ItemDropSetting> itemDropSettings = new List<ItemDropSetting>();

        #region 公開方法
        public DropSettingBase CalDrop(EnemyObject enemy)
        {
            var enemySetting = enemy.entitySetting as Enemy;
            var levelOffset = (float)enemy.level / 100f;
            float totalDropRate = dropRate.Evaluate(levelOffset);

            if (Random.Range(0f, 1f) >= totalDropRate)
                return null;

            List<System.Object> allDrop = new List<System.Object>();
            allDrop.AddRange(modDropSettings);
            allDrop.AddRange(itemDropSettings);

            // (掉落率 * n)放入陣列中抽一個出來
            List<int> prizeList = new List<int>();
            for (int i = 0; i < allDrop.Count; ++i)
            {
                var item = allDrop[i] as DropSettingBase;
                int amout = (int)Mathf.Floor(item.dropRate.Evaluate(levelOffset) * 100);
                for (int x = 0; x < amout; ++x)
                    prizeList.Add(i);
            }

            return (DropSettingBase)allDrop[prizeList[Random.Range(0, prizeList.Count)]];
        }


        // 換算所有掉落率
        // index = 0 不掉落
        public List<string> AllDropRateStatus(EnemyObject enemy)
        {
            List<string> result = new List<string>();

            var enemySetting = enemy.entitySetting as Enemy;
            var levelOffset = (float)enemy.level / 100f;
            float totalDropRate = dropRate.Evaluate(levelOffset);
            result.Add($"不掉落：{((1 - totalDropRate) * 100).ToString("f2")}%");

            List<DropSettingBase> allDrop = new List<DropSettingBase>();
            allDrop.AddRange(modDropSettings);
            allDrop.AddRange(itemDropSettings);

            float allDropRateSum = 0f;
            foreach (var drop in allDrop)
                allDropRateSum += drop.dropRate.Evaluate(levelOffset);

            for (int i = 0; i < allDrop.Count; ++i)
            {
                var item = allDrop[i];
                float rate = (item.dropRate.Evaluate(levelOffset) / allDropRateSum) * (totalDropRate);

                result.Add($"{item.name}：{(rate * 100).ToString("f2")}%");
            }

            return result;
        }

        #endregion
    }
}

