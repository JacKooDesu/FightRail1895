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
        public Text money;
        public Text asset;


        [Header("車廂面板")]
        public TowerPanel towerPanel;
        public TrainPanel trainPanel;

        public GameObject trainHeadUIPrefab;
        public GameObject trainCabinUIPrefab;

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
            // money
            UpdateState();
        }

        void UpdateState()
        {
            healthBar.value = trackingTrain.health / trackingTrain.maxHealth;
        }
    }
}
