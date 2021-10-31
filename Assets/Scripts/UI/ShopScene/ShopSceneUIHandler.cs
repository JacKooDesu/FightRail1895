﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JacDev.Utils;
using JacDev.Audio;

namespace JacDev.UI.ShopScene
{
    public class ShopSceneUIHandler : MonoBehaviour
    {
        public EventTrigger backButton;

        // Start is called before the first frame update
        void Start()
        {
            EventBinder.Bind(
                backButton,
                EventTriggerType.PointerEnter,
                (data) => AudioHandler.Singleton.PlaySound("hover")
            );
            
            EventBinder.Bind(
                backButton,
                EventTriggerType.PointerClick,
                (data) => AudioHandler.Singleton.PlaySound("select")
            );
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}