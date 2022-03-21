using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Tutorial
{
    [CreateAssetMenu(fileName = "new Tutorial", menuName = "JacDev/Tutorial/TutorialSetting", order = 0)]
    public class TutorialSetting : ScriptableObject
    {
        public string title;    // 教學標題
        public int id;  // 教學ID，供後續呼叫

        public TutorialBase[] tutorials;
    }

    [System.Serializable]
    public class TutorialBase
    {
        [TextArea(2,10)]
        public string description;  // 說明
        public Sprite image;    // 圖片
    }
}
