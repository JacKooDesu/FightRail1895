﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacDev.Utils.UISlicker
{
    [AddComponentMenu("JacDev/UI Slicker/Color Slicker")]
    public class ColorSlicker : SlickerBase
    {
        [System.Serializable]   // 不知道怎麼解決，無法序列化泛型
        public class ColorSetting : Setting<Color> { }
        public List<ColorSetting> settings = new List<ColorSetting>();
        public ColorSetting origin = new ColorSetting();

        private void OnEnable()
        {
            origin.Init("origin", GetComponent<Graphic>().color, origin.time);
        }

        public override void Slick(string name)
        {
            foreach (Setting<Color> c in settings)
            {
                if (c.name == name)
                {
                    Tween<Color>(c, GetComponent<Graphic>().color);
                }
            }
        }

        public override void SlickBack()
        {
            Tween<Color>(origin, GetComponent<Graphic>().color);
        }

        protected void TweenCallback(Color value)
        {
            GetComponent<Graphic>().color = value;
        }
    }

}
