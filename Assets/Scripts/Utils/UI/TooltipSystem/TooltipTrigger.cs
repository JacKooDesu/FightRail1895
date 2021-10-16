using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JacDev.Utils.TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool clickHide = false;
        public string header;
        [TextArea(3, 10)]
        public string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(content, header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clickHide)
                TooltipSystem.Hide();
        }
    }
}

