using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Utils.UISlicker
{
    public class PositionSlick : SlickerBase
    {
        [System.Serializable]
        public class PositionSetting : Setting<Vector2> { }
        public List<PositionSetting> settings = new List<PositionSetting>();

        public PositionSetting origin = new PositionSetting();

        private void OnEnable()
        {
            origin.Init("origin", rect.anchoredPosition, origin.time);
        }

        public override void Slick(string name)
        {
            base.Slick(name);
            foreach (PositionSetting v in settings)
            {
                if (v.name == name)
                {
                    Tween<Vector2>(v, rect.anchoredPosition);
                }
            }
        }

        public override void SlickBack()
        {
            base.SlickBack();
            Tween<Vector2>(origin, rect.anchoredPosition);
        }

        protected void TweenCallback(Vector2 value)
        {
            rect.anchoredPosition = value;
        }
    }

}
