using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Tutorial
{
    [CreateAssetMenu(fileName = "TutorialList", menuName = "JacDev/Tutorial/TutorialList", order = 0)]
    public class TutorialList : ScriptableObject
    {
        public List<TutorialSetting> tutorialSettings = new List<TutorialSetting>();

        public TutorialSetting GetByID(int id)
        {
            if (tutorialSettings.Find((ts) => ts.id == id) != null)
            {
                return tutorialSettings.Find((ts) => ts.id == id);
            }
            else
            {
                UnityEngine.Debug.LogError("未知id");
                return null;
            }
        }
    }

}
