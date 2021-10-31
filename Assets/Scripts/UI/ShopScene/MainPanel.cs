using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JacDev.Utils;
using JacDev.Audio;

namespace JacDev.UI.ShopScene
{
    public class MainPanel : MonoBehaviour
    {
        public Transform optionParent;

        private void Start()
        {
            foreach (Transform t in optionParent)
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
