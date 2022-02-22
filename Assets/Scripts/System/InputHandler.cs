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
            if ((int)this.state == i)
            {
                actionSetups[(int)state].OnEnd();
                actionSetups[(int)i].OnBegin();
            }

            this.state = (InputState)i;
        }

        public void SetState(InputState i)
        {
            if (this.state == i)
            {
                actionSetups[(int)state].OnEnd();
                actionSetups[(int)i].OnBegin();
            }

            this.state = i;
        }

        #region EVENTS
        public class ActionSetupBase
        {
            public event Action onBegin;
            public void OnBegin()
            {
                if (onBegin != null)
                    onBegin();
            }

            public event Action onUpdate;
            public void OnUpdate()
            {
                if (onUpdate != null)
                    onUpdate();
            }

            public event Action onEnd;
            public void OnEnd()
            {
                if (onEnd != null)
                    onEnd();
            }
        }

        public ActionSetupBase normalEvent = new ActionSetupBase();
        public ActionSetupBase placingTowerEvent = new ActionSetupBase();
        public ActionSetupBase selectTowerEvent = new ActionSetupBase();

        ActionSetupBase[] actionSetups;
        #endregion

        private void Start()
        {
            actionSetups = new ActionSetupBase[]{
                normalEvent,
                placingTowerEvent,
                selectTowerEvent
            };

            actionSetups[(int)state].OnBegin();
        }

        void Update()
        {
            if (lastState != state)
            {
                actionSetups[(int)lastState].OnEnd();

                lastState = state;

                actionSetups[(int)state].OnBegin();
            }
            else
            {
                actionSetups[(int)state].OnUpdate();
            }
        }
    }
}

