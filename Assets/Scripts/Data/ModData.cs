using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Mod;
using JacDev.Entity;

namespace JacDev.Data
{
    [System.Serializable]
    public class ModData
    {
        public int modSettingIndex;  // Mod Setting index(mod type)
        public int[] setIndex = { -1, -1, -1 };   // 配置位置
        public int[] SetIndex
        {
            get
            {
                if (setIndex.Length != 3)
                {
                    System.Array.Resize(ref setIndex, 3);
                    setIndex[0] = -1;
                    setIndex[1] = -1;
                    setIndex[2] = -1;
                }

                return setIndex;
            }
            set
            {
                setIndex = value;
            }
        }
        public int rank;

        public Mod.ModFactory ToModFactory()
        {
            return SettingManager.Singleton.ModList.modList[modSettingIndex];
        }
    }
}
