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
            g.GetComponent<UnityEngine.UI.Text>().text = ((int)damage).ToString();

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