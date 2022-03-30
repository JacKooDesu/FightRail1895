using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;
using JacDev.Utils.UISlicker;

namespace JacDev.UI.GameScene
{
    public class DamagePanel : MonoBehaviour
    {
        public GameObject damagePrefab;
        public bool display = true;
        public Vector2 positionOffset;
        public Vector2 moveAmount;

        public void DisplayDamage(EntityObject entity, float damage)
        {
            GameObject g = Instantiate(damagePrefab, Camera.main.WorldToScreenPoint(entity.transform.position), Quaternion.identity, transform);

            var text = g.GetComponent<UnityEngine.UI.Text>();
            int i = (int)damage;
            float f = damage - i;
            string finalStr = i.ToString();
            if (f != 0f)
                finalStr += $".<size={text.fontSize *.8f}>{f.ToString("f1")[2]}</size>";
            text.text = finalStr;

            PositionSlick.PositionSetting positionSetting = new PositionSlick.PositionSetting();
            positionSetting.name = "FadeOut";
            positionSetting.time = .5f;
            positionSetting.set = (g.transform as RectTransform).anchoredPosition + moveAmount;
            positionSetting.onComplete += () => { Destroy(g); };
            g.GetComponent<PositionSlick>().settings.Add(positionSetting);

            g.GetComponent<AlphaSlicker>().Slick("FadeOut");
            g.GetComponent<PositionSlick>().Slick("FadeOut");
        }
    }
}