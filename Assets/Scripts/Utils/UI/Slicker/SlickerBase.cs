using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

        public virtual void Slick()
        {
            if (GetComponent<iTween>())
                iTween.Stop(gameObject);

        }

        public virtual void Slick(string name)
        {
            if (GetComponent<iTween>())
                iTween.Stop(gameObject);
        }
        public virtual void SlickBack()
        {
            if (GetComponent<iTween>())
                iTween.Stop(gameObject);
        }

        // protected void Tween<T>(Setting<T> s, T from)
        // {
        //     iTween.ValueTo(gameObject,
        //         iTween.Hash(
        //         "from", from,
        //         "to", s.set,
        //         "time", s.time,
        //         "easetype", s.easeType,
        //         "onupdate", "TweenCallback"
        //         ));
        // }
    }

    [System.Serializable]
    public class Setting<T>
    {
        public string name;
        [SerializeField] public T set;
        public float time = .2f;
        public Ease easeType = Ease.OutQuad;

        public void Init(string name, T set, float time)
        {
            this.name = name;
            this.set = set;
            this.time = time;
        }
    }
}

