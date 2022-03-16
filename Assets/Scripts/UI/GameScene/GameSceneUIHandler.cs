using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using JacDev.Entity;

namespace JacDev.UI.GameScene
{
    public class GameSceneUIHandler : MonoBehaviour
    {
        static GameSceneUIHandler singleton = null;
        public static GameSceneUIHandler Singleton
        {
            get
            {
                singleton = FindObjectOfType(typeof(GameSceneUIHandler)) as GameSceneUIHandler;

                if (singleton == null)
                {
                    GameObject g = new GameObject("GameSceneUIHandler");
                    singleton = g.AddComponent<GameSceneUIHandler>();
                }

                return singleton;
            }
        }

        public TrainObject trackingTrain;
        [Header("車廂狀態")]
        public Slider healthBar;
        public Slider fuel;
        public Text money;
        public Text asset;
        public Text speed;


        [Header("車廂面板")]
        public TowerPanel towerPanel;
        public TrainPanel trainPanel;

        public GameObject trainHeadUIPrefab;
        public GameObject trainCabinUIPrefab;

        [Header("傷害面板")]
        public DamagePanel damagePanel;

        [Header("音樂選擇")]
        public MusicSelectPanel musicSelectPandel;

        [Header("暫停")]
        public EventTrigger pauseButton;
        public GameObject pausePanel;
        public EventTrigger continueButton;
        public EventTrigger settingButton;
        public EventTrigger mainMenuButton;

        private void OnEnable()
        {
            if (GameHandler.Singleton.debugMode)
                return;
                
            JacDev.Utils.EventBinder.Bind(pauseButton, EventTriggerType.PointerClick,
                (e) => { GameHandler.Singleton.Pause(true); pausePanel.SetActive(true); }
            );
            JacDev.Utils.EventBinder.Bind(continueButton, EventTriggerType.PointerClick,
                (e) => { GameHandler.Singleton.Pause(false); pausePanel.SetActive(false); }
            );
            JacDev.Utils.EventBinder.Bind(mainMenuButton, EventTriggerType.PointerClick,
                (e) => { AsyncSceneLoader.Singleton.LoadScene("Title_v2"); }
            );
        }

        public void InitTrainPanel()
        {

        }

        public void ToggleLowerPanel(int index)
        {
            switch (index)
            {
                case 0:
                    towerPanel.gameObject.SetActive(true);
                    trainPanel.gameObject.SetActive(false);
                    break;

                case 1:
                    towerPanel.gameObject.SetActive(false);
                    trainPanel.gameObject.SetActive(true);
                    break;
            }
        }

        public void ToggleCabin(int index)
        {

        }

        private void Update()
        {
            if (GameHandler.Singleton.debugMode)
                return;
            // money
            UpdateState();
        }

        void UpdateState()
        {
            healthBar.value = trackingTrain.health / trackingTrain.maxHealth;
            healthBar.GetComponentInChildren<Text>().text = $"{trackingTrain.health} / {trackingTrain.maxHealth}";
            money.text = GameHandler.Singleton.money.ToString();
        }
    }
}
