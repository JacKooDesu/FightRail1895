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

        string modName = "";
        string description = "";

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
                var qualityFactory = SettingManager.Singleton.ModList.qualityFactory;
                var data = modPanel.modTargetData[index];
                icon.sprite = data.ToModFactory().icon;
                bg.sprite = qualityFactory.GetQualitySetting(data.ToModFactory()).GetQualityIcon(data.rank);
                bg.color = new Color(1, 1, 1);

                icon.gameObject.SetActive(true);

                equipUI.SetActive(!isInstance && data.SetIndex[modPanel.currentSet] != -1);

                SetDescription();
            }
        }

        void SetDescription()
        {
            modName = "";
            description = "";
            var data = modPanel.modTargetData[modIndex];
            modName = $"{data.ToModFactory().modName}";
            description += $"{data.ToModFactory().description} ";

            float value = data.ToModFactory().GetQualityValue(data.rank);
            string color = value >= 0 ? "#ffd500" : "#ff0090";
            description += $"<color={color}>{value}</color>";
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
            {
                Audio.AudioHandler.Singleton.PlaySound("hover");
                modPanel.modName.text = modName;
                modPanel.modDescription.text = description;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (modIndex != -1)
            {
                modPanel.modName.text = "";
                modPanel.modDescription.text = "";
            }
        }
    }

}
