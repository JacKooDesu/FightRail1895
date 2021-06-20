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
            Normal,
            PlacingTower
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
            public event Action onBegin;
            public void OnBegin()
            {
                if (onBegin != null)
                    onBegin();
            }

            public event Action onUpdate;
            public IEnumerator OnUpdate()
            {
                while (true)
                {
                    if (onUpdate != null)
                        onUpdate();

                    yield return null;
                }
            }

            public event Action onEnd;
            public void OnEnd()
            {
                if (onEnd != null)
                    onEnd();
            }
        }

        public ActionSetup normalEvent = new ActionSetup();
        public ActionSetup placingTowerEvent = new ActionSetup();
        #endregion

        private void Start()
        {
            switch (state)
            {
                case InputState.Normal:
                    normalEvent.OnBegin();
                    StartCoroutine(normalEvent.OnUpdate());
                    break;

                case InputState.PlacingTower:
                    placingTowerEvent.OnBegin();
                    StartCoroutine(placingTowerEvent.OnUpdate());
                    break;

                default:
                    break;
            }
        }

        void Update()
        {
            if (lastState != state)
            {
                switch (lastState)
                {
                    case InputState.Normal:
                        StopCoroutine(normalEvent.OnUpdate());
                        normalEvent.OnEnd();
                        break;

                    case InputState.PlacingTower:
                        StopCoroutine(placingTowerEvent.OnUpdate());
                        placingTowerEvent.OnEnd();
                        break;

                    default:
                        break;
                }

                lastState = state;

                switch (state)
                {
                    case InputState.Normal:
                        normalEvent.OnBegin();
                        StartCoroutine(normalEvent.OnUpdate());
                        break;

                    case InputState.PlacingTower:
                        placingTowerEvent.OnBegin();
                        StartCoroutine(placingTowerEvent.OnUpdate());
                        break;

                    default:
                        break;
                }
            }
        }
    }
}

