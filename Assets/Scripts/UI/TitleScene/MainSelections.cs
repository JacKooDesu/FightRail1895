using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils;
using UnityEngine.EventSystems;

namespace JacDev.UI.TitleScene
{
    public class MainSelections : MonoBehaviour
    {
        public GameObject startGame;
        public GameObject continueGame;
        public GameObject setting;
        public GameObject developers;
        public GameObject leave;

        public void BindSound()
        {
            List<GameObject> objects = new List<GameObject>() { startGame, continueGame, setting, developers, leave };

            foreach (GameObject g in objects)
            {
                EventBinder.Bind(
                g.GetComponent<EventTrigger>(),
                EventTriggerType.PointerEnter,
                (data) => { Audio.AudioHandler.Singleton.PlaySound("hover"); }
                );

                EventBinder.Bind(
                g.GetComponent<EventTrigger>(),
                EventTriggerType.PointerClick,
                (data) => { Audio.AudioHandler.Singleton.PlaySound("select"); }
                );
            }
        }
    }
}
