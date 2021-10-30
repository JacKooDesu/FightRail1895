using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Audio;
using JacDev.Data;
using UnityEngine.UI;

namespace JacDev.UI.GameScene
{
    public class MusicSelectPanel : MonoBehaviour
    {
        public Transform contentPanel;
        public GameObject selectionPrefab;

        List<GameObject> selections = new List<GameObject>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            foreach (SoundSetting s in SettingManager.Singleton.BgmSetting.soundSettings)
            {
                GameObject g = Instantiate(selectionPrefab, contentPanel);
                g.GetComponentInChildren<Button>().onClick.AddListener(
                    () =>
                    {
                        AudioHandler.Singleton.PlayBgm(s.name);
                        UpdateState();
                    });
                g.GetComponentInChildren<Text>().text = s.name;

                selections.Add(g);
            }
            UpdateState();
        }

        void UpdateState()
        {
            foreach (GameObject g in selections)
                g.GetComponent<Image>().enabled = false;

            if (AudioHandler.Singleton.currentPlayingBgm != -1)
                selections[AudioHandler.Singleton.currentPlayingBgm].GetComponent<Image>().enabled = true;
        }
    }
}