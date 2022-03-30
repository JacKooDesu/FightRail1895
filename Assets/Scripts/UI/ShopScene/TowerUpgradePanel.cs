using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JacDev.Shop;

namespace JacDev.UI.ShopScene
{
    public class TowerUpgradePanel : MonoBehaviour
    {
        [Header("狀態")]
        public Text towerName;
        public Text towerNickName;
        public Text towerLevel;
        public Text towerInfo;
        public Transform towerLevelIconParent;
        List<Image> towerLevelIcon = new List<Image>();

        [Header("側欄")]
        public Transform towerSelectPanel;
        [SerializeField] List<Transform> towerSelections = new List<Transform>();

        [Header("按鈕")]
        public Button levelUpButton;

        [Header("設定")]
        public Color lockedColor;
        public Color disableColor;

        public int currentSelect = 0;

        TowerPreviewer previewer;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform t in towerLevelIconParent)
                towerLevelIcon.Add(t.GetComponent<Image>());

            foreach (Transform t in towerSelectPanel)
                towerSelections.Add(t);

            for (int i = 0; i < towerSelections.Count; ++i)
            {
                if (i < SettingManager.Singleton.TowerSetting.towers.Count)
                {
                    towerSelections[i].Find("Icon").GetComponent<Image>().sprite = SettingManager.Singleton.TowerSetting.towers[i].icon;

                    towerSelections[i].Find("Mask").GetComponent<Image>().color =
                        DataManager.Singleton.PlayerData.towersGrade[i] == 0 ? lockedColor : new Color(0, 0, 0, 0);

                    if (DataManager.Singleton.PlayerData.towersGrade[i] == 0)
                        continue;
                    int x = i;
                    towerSelections[i].GetComponent<Button>().onClick.AddListener(
                        () =>
                        {
                            currentSelect = x;
                            UpdateState();
                        }
                    );

                }
                else
                {
                    towerSelections[i].Find("Mask").GetComponent<Image>().color = disableColor;
                }
            }

            previewer = FindObjectOfType<TowerPreviewer>();
            previewer.Init(this);

            UpdateState();

            levelUpButton.onClick.AddListener(() => LevelUp());
        }

        public void UpdateState()
        {
            JacDev.Data.PlayerData data = DataManager.Singleton.PlayerData;
            towerName.text = SettingManager.Singleton.TowerSetting.towers[currentSelect].entityName;
            towerNickName.text = SettingManager.Singleton.TowerSetting.towers[currentSelect].entityNickName;

            towerInfo.text = SettingManager.Singleton.TowerSetting.towers[currentSelect].entityInfo;

            for (int i = 0; i < towerLevelIcon.Count; ++i)
            {
                if (i < data.towersGrade[currentSelect])
                    towerLevelIcon[i].color = Color.white;
                else
                    towerLevelIcon[i].color = lockedColor;
            }

            towerLevel.text = data.towersGrade[currentSelect].ToString();

            previewer.ChangeModel(currentSelect);
        }

        private void OnEnable()
        {
            if (previewer != null)
                UpdateState();
        }

        private void OnDisable()
        {
            previewer.ChangeModel(-1);
        }

        public void LevelUp()
        {
            var towerGrades = DataManager.Singleton.PlayerData.towersGrade;
            towerGrades[currentSelect] += 1;
            previewer.LevelUp();
            UpdateState();
        }
    }
}