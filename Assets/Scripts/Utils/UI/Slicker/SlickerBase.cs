using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Utils.UISlicker
{
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

        public virtual void SlickBack() { }
    }
}

