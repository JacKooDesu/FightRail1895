using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;
using UnityEngine.EventSystems;

namespace JacDev.UI.TitleScene
{
    public class BloodSelect : MonoBehaviour
    {
        public Transform bloodCellParent;

        public EventTrigger backButton;

        private void OnEnable()
        {
            for (int i = 0; i < bloodCellParent.childCount; ++i)
            {
                int x = i;
                EventTrigger trigger = bloodCellParent.GetChild(i).GetComponent<EventTrigger>();
                EventBinder.Bind(
                    trigger,
                    EventTriggerType.PointerClick,
                    (data) => { Audio.AudioHandler.Singleton.PlaySound("begin"); GameHandler.Singleton.NewGame(x); }
                    );
            }

            EventBinder.Bind(
                backButton,
                EventTriggerType.PointerClick,
                (data) => { Audio.AudioHandler.Singleton.PlaySound("select"); }
                );

            EventBinder.Bind(
                backButton,
                EventTriggerType.PointerEnter,
                (data) => { Audio.AudioHandler.Singleton.PlaySound("hover"); }
                );
        }
    }

}
