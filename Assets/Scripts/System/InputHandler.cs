using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.GameSystem
{
    public class InputHandler : MonoBehaviour
    {
        static InputHandler singleton = null;

        public static InputHandler Singleton
        {
            get
            {
                singleton = FindObjectOfType(typeof(InputHandler)) as InputHandler;

                if (singleton == null)
                {
                    GameObject g = new GameObject("InputHandler");
                    singleton = g.AddComponent<InputHandler>();
                }

                return singleton;
            }
        }

        public enum InputState
        {
            Normal = 0,
            PlacingTower,
            SelectTower
        }
        [SerializeField] InputState state;
        InputState lastState = InputState.Normal;
        public InputState State
        {
            set
            {
                state = value;
            }
            get
            {
                return state;
            }
        }

        public void SetState(int i)
        {
            this.state = (InputState)i;
        }

        #region EVENTS
        public class ActionSetup
        {
            public Action onBegin;

            public Action onUpdate;

            public Action onEnd;
        }

        public ActionSetup normalEvent = new ActionSetup();
        public ActionSetup placingTowerEvent = new ActionSetup();
        public ActionSetup selectTowerEvent = new ActionSetup();

        ActionSetup[] actionSetups;
        #endregion

        private void Start()
        {
            actionSetups = new ActionSetup[]{
                normalEvent,
                placingTowerEvent,
                selectTowerEvent
            };

            switch (state)
            {
                case InputState.Normal:
                    if (normalEvent.onBegin != null)
                        normalEvent.onBegin.Invoke();
                    break;

                case InputState.PlacingTower:
                    if (placingTowerEvent.onBegin != null)
                        placingTowerEvent.onBegin.Invoke();
                    break;

                case InputState.SelectTower:
                    if (selectTowerEvent.onBegin != null)
                        selectTowerEvent.onBegin.Invoke();
                    break;

                default:
                    break;
            }
        }

        void Update()
        {
            if (lastState != state)
            {
                if (actionSetups[(int)lastState].onEnd != null)
                    actionSetups[(int)lastState].onEnd.Invoke();

                lastState = state;

                if (actionSetups[(int)state].onBegin != null)
                    actionSetups[(int)state].onBegin.Invoke();
            }

            if (actionSetups[(int)state].onUpdate != null)
                actionSetups[(int)state].onUpdate.Invoke();
        }
    }
}

