using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JacDev.Utils.TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header, content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(content, header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
    }
}

