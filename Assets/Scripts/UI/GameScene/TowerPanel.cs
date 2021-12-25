using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils.UISlicker;
using JacDev.GameSystem;
using JacDev.Data;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JacDev.UI.GameScene
{
    public class TowerPanel : MonoBehaviour
    {
        PositionSlick positionSlick;

        public Transform parentUI;
        public Transform prefabUI;

        Hashtable hashtable = new Hashtable();

        private void Start()
        {
            var playerData = DataManager.Singleton.PlayerData;
            var towerSetting = SettingManager.Singleton.TowerSetting;

            int currentTower = 0;
            foreach (Transform t in parentUI)
            {
                print(t.name);
                while (currentTower < towerSetting.towers.Count && playerData.towersGrade[currentTower] == 0)
                    currentTower++;

                if (currentTower < towerSetting.towers.Count)
                {
                    int towerIndex = currentTower;
                    var tower = towerSetting.towers[towerIndex];
                    Utils.EventBinder.Bind(t.GetComponent<EventTrigger>(), EventTriggerType.BeginDrag, (e) =>
                    {
                        FindObjectOfType<Entity.TowerSpawner>().SelectTower = towerIndex;
                    });
                    t.Find("BG").GetComponent<Image>().sprite = tower.icon;
                    t.GetComponentInChildren<Text>().text = tower.price.ToString();

                    hashtable.Add(t.Find("Cover"), tower);
                    currentTower++;
                }
                else
                {
                    t.Find("BG").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    t.GetComponentInChildren<Text>().text = "--";
                }
            }


            positionSlick = GetComponent<PositionSlick>();
            // InputHandler.Singleton.placingTowerEvent.onBegin += () => positionSlick.Slick("hide");
            // InputHandler.Singleton.placingTowerEvent.onEnd += () => positionSlick.SlickBack();


        }

        private void Update()
        {
            foreach (DictionaryEntry h in hashtable)
            {
                ((Transform)h.Key).gameObject.SetActive(((Entity.Tower)h.Value).price > GameHandler.Singleton.money);
            }
        }

    }

}
