using UnityEngine;
using System.Collections;

namespace JacDev.Utils.UISlicker
{
    [AddComponentMenu("JacDev/UI Slicker/Size Slicker")]
    public class SizeSlicker : SlickerBase
    {
        public Vector2 origin;
        public Vector2 final;
        public float speed = 5f;

        private void OnEnable()
        {
            if (origin == Vector2.zero)
            {
                origin = rect.sizeDelta;
            }
        }

        public override void Slick()
        {
            base.Slick();
            StopAllCoroutines();
            StartCoroutine(UpdateSize(final));
        }

        public override void SlickBack()
        {
            base.SlickBack();
            StopAllCoroutines();
            StartCoroutine(UpdateSize(origin));
        }

        IEnumerator UpdateSize(Vector2 target)
        {
            while ((rect.sizeDelta - final).magnitude >= .001f)
            {
                rect.sizeDelta = iTween.Vector2Update(rect.sizeDelta, target, speed);
                yield return null;
            }
            rect.sizeDelta = target;
        }
    }
}