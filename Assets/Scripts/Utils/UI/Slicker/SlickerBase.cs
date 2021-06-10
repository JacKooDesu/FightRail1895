using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Utils.UISlicker
{
    [System.Serializable]
    public class SlickerBase : MonoBehaviour
    {
        protected RectTransform rect
        {
            get
            {
                return this.transform as RectTransform;
            }
        }

        public virtual void Slick() { }

        public virtual void Slick(string name) { }
        public virtual void SlickBack() { }

        protected void Tween<T>(Setting<T> s, T from)
        {
            {
                iTween.ValueTo(gameObject,
                    iTween.Hash(
                    "from", from,
                    "to", s.set,
                    "time", s.time,
                    "easetype", iTween.EaseType.linear,
                    "onupdate", "TweenCallback"
                    ));
            }
        }

        //protected virtual void TweenCallback<T>(T value) { }
    }

    [System.Serializable]
    public class Setting<T>
    {
        public string name;
        [SerializeField]public T set;
        public float time;

        public void Init(string name, T set, float time)
        {
            this.name = name;
            this.set = set;
            this.time = time;
        }
    }
}

