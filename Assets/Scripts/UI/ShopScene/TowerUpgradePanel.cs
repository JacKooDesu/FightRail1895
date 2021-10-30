using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        List<Transform> towerSelections = new List<Transform>();

        [Header("設定")]
        public Color lockedColor;
        public Color disableColor;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform t in towerLevelIconParent)
                towerLevelIcon.Add(t.GetComponent<Image>());

        }

        public void UpdateState()
        {

        }
    }
}