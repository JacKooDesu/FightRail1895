using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JacDev.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        // Singleton
        static TutorialManager singleton = null;

        public static TutorialManager Singleton
        {
            get
            {
                if (singleton != null)
                    return singleton;
                else
                    singleton = FindObjectOfType(typeof(TutorialManager)) as TutorialManager;

                if (singleton == null)
                {
                    GameObject g = new GameObject("TutorialManager");
                    singleton = g.AddComponent<TutorialManager>();
                }
                return singleton;
            }
        }

        [Header("按鈕")]
        public Button nextBtn;
        public Button backBtn;
        public Button confirmBtn;
        [Header("物件綁定")]
        public Transform parent;    // 用於DoTween動畫
        public Transform contentParent;
        ObjectSwitcher contentSwitcher;
        public Text titleText;

        [Header("設定")]
        public TutorialUI prefab;
        public float duration;

        public List<TutorialUI> tutorialUis = new List<TutorialUI>();

        private void Start()
        {
            if (contentSwitcher == null)
                BindSwitcher();

            confirmBtn.onClick.AddListener(() =>
            {
                parent.DOScale(Vector3.zero, duration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
                {
                    parent.gameObject.SetActive(false);
                    ResetAll();
                    Time.timeScale = 1f;
                });
            });
        }

        void BindSwitcher()
        {
            contentSwitcher = contentParent.GetComponent<ObjectSwitcher>();

            contentSwitcher.OnSwitch += (newIndex, oldIndex) =>
            {
                int length = contentSwitcher.objects.Count;

                nextBtn.interactable = true;
                backBtn.interactable = true;
                if (newIndex == length - 1)
                    nextBtn.interactable = false;
                else if (newIndex == 0)
                    backBtn.interactable = false;
            };

            nextBtn.onClick.AddListener(() => contentSwitcher.Next());
            backBtn.onClick.AddListener(() => contentSwitcher.Back());
        }

        public void Tutorial(int id)
        {
            var pData = DataManager.Singleton.PlayerData;
            if (pData.tutorialData.Contains(id))
                return;

            if (contentSwitcher == null)
                BindSwitcher();

            Time.timeScale = 0f;
            parent.localScale = Vector3.zero;
            parent.DOScale(Vector3.one, duration).SetEase(Ease.OutBack).SetUpdate(true);
            parent.gameObject.SetActive(true);

            var setting = SettingManager.Singleton.TutorialList.GetByID(id);

            UnityEngine.Debug.Log($"正在教學 {setting.title}");

            titleText.text = setting.title;
            foreach (var ui in setting.tutorials)
            {
                var t = Instantiate(prefab, contentParent);
                t.Setup(ui.description, ui.image);

                contentSwitcher.objects.Add(t.gameObject);
                tutorialUis.Add(t);
            }

            contentSwitcher.Switch(0);

            pData.tutorialData.Add(id);
        }

        // 用於關閉時使用
        void ResetAll()
        {
            if (contentSwitcher == null)
                BindSwitcher();

            contentSwitcher.objects = new List<GameObject>();
            tutorialUis = new List<TutorialUI>();

            for (int i = contentParent.childCount; i > 0; --i)
                Destroy(contentParent.GetChild(i - 1).gameObject);
        }
    }
}