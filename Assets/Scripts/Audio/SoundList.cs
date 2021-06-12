using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Audio
{
    [System.Serializable, CreateAssetMenu(fileName = "Sound List", menuName = "JacDev/Create Sound List", order = 1)]
    public class SoundList : ScriptableObject
    {

        public SoundSetting[] soundSettings;

        internal SoundSetting GetSound(string name)
        {
            foreach (SoundSetting s in soundSettings)
            {
                if (s.name == name)
                    return s;
            }
            return null;
        }
    }

    [System.Serializable]
    public class SoundSetting
    {
        public string name;
        public AudioClip clip;
    }
}

