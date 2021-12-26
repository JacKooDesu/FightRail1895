using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JacDev.UI.ShopScene.Mod
{
    public class ModUIObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        ModPanel modPanel;
        public bool isInstance;
        public int modIndex = -1;   // 由此讀取ModData，再由SettingManager轉ModSetting。(-1為空)

        [Header("UI設定")]
        public Image icon;
        public Image bg;
        public GameObject equipUI;

        public void Init(ModPanel modPanel, int index)
        {
            this.modPanel = modPanel;
            modIndex = index;
            if (index == -1)
            {
                icon.sprite = null;
                icon.gameObject.SetActive(false);
                bg.sprite = null;
                bg.color = new Color(0, 0, 0, .5f);
                equipUI.SetActive(false);
            }
            else
            {
                var data = modPanel.modTargetData[index];
                icon.sprite = data.ToModFactory().icon;
                bg.sprite = data.ToModFactory().GetRankSetting(data.rank).icon;
                bg.color = new Color(1, 1, 1);

                icon.gameObject.SetActive(true);

                equipUI.SetActive(!isInstance && data.SetIndex[modPanel.currentSet] != -1);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (modIndex == -1)
                return;

            if (isInstance)
            {
                modPanel.RemoveModFromBlank(this);
            }
            else
            {
                modPanel.AddModToBlank(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (modIndex != -1)
                Audio.AudioHandler.Singleton.PlaySound("hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }

}
