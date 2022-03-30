using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;
using JacDev.UI.ShopScene;
using DG.Tweening;

namespace JacDev.Shop
{
    public class TowerPreviewer : MonoBehaviour
    {
        TowerUpgradePanel towerUpgradePanel;
        public float rotateSpeed = 10;

        Transform child;    // 裝模型的子物件
        Transform[] models; // 裝塔模型的地方
        int current = -1;
        [SerializeField] ParticleSystem levelUpEffect;

        public void Init(TowerUpgradePanel towerUpgradePanel)
        {
            this.towerUpgradePanel = towerUpgradePanel;
            child = new GameObject("Child").transform;
            child.SetParent(transform);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.zero;

            InitModels();
        }

        void InitModels()
        {
            var towerSetting = SettingManager.Singleton.TowerSetting;
            models = new Transform[towerSetting.towers.Count];

            for (int i = 0; i < models.Length; ++i)
            {
                var tower = Instantiate(towerSetting.towers[i].prefab.transform.GetChild(0), child).transform;
                // Destroy(tower.GetComponent<TowerObject>());
                models[i] = tower;

                tower.gameObject.SetActive(false);
            }
        }

        public void ChangeModel(int index)
        {
            if (index == current)
                return;

            if (index == -1)
            {
                child.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack);
                current = index;
                return;
            }

            if (child.localScale != Vector3.zero)
            {
                child.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    models[current].gameObject.SetActive(false);
                    models[index].gameObject.SetActive(true);
                    current = index;
                    child.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
                });
            }
            else
            {
                models[index].gameObject.SetActive(true);
                child.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
                current = index;
            }
        }

        private void Update()
        {
            if (towerUpgradePanel == null)
                return;
            child.eulerAngles += Time.deltaTime * rotateSpeed * Vector3.up;
        }


        public void LevelUp()
        {
            levelUpEffect.Play();
        }
    }

}
