using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JacDev.Utils;
using JacDev.Utils.UISlicker;

namespace JacDev.UI.TitleScene
{
    public class TitleSceneUIHandler : MonoBehaviour
    {
        public MainSelections mainSelections;
        public GameObject hintUI;

        void Start()
        {
            BindEvents();
        }

        void BindEvents()
        {
            hintUI.GetComponent<ColorSlicker>().settings[0].onComplete += () =>
            {
                StartCoroutine(EventBinder.FunctionDelay(3.5f, () => { hintUI.GetComponent<ColorSlicker>().SlickBack(); }));
            };

            EventBinder.Bind(
                mainSelections.continueGame.GetComponent<EventTrigger>(),
                EventTriggerType.PointerClick,
                (data) =>
                {
                    if (DataManager.Singleton.PlayerData != null)
                    {
                        AsyncSceneLoader.Singleton.LoadScene("ShopScene");
                    }
                    else
                    {
                        hintUI.GetComponentInChildren<Text>().text = "尚無存檔資料";
                        hintUI.GetComponent<ColorSlicker>().Slick("in");
                    }
                });

            mainSelections.BindSound();
        }
    }
}

