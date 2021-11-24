using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JacDev.Utils;
using JacDev.Audio;

namespace JacDev.UI.ShopScene
{
    public class TradePanel : MonoBehaviour
    {
        [Header("側欄")]
        public Transform sideSelector;

        void Start()
        {
            foreach (Transform t in sideSelector)
            {
                EventBinder.Bind(
                    t.GetComponent<EventTrigger>(),
                    EventTriggerType.PointerEnter,
                    (data) => AudioHandler.Singleton.PlaySound("hover")
                );

                EventBinder.Bind(
                    t.GetComponent<EventTrigger>(),
                    EventTriggerType.PointerClick,
                    (data) => AudioHandler.Singleton.PlaySound("select")
                );
            }
        }
    }
}