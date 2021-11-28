using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using JacDev.Audio;

namespace JacDev.UI.ShopScene
{
    public class ShoppingPanel : MonoBehaviour
    {
        public Transform itemsParent;
        public Text selectingItemName;
        public Image selectingItemImage;
        public Text selectingItemPrice;
        public Text selectingItemAmount;
        int itemAmount = 1;
        public Text selectingItemDescription;

        [Header("按鈕")]
        public EventTrigger addAmountButton;
        public EventTrigger subAmountButton;

        [Header("3D預覽")]
        public Transform itemViewerParent;

        List<Item.ItemSetting> itemList = new List<Item.ItemSetting>();

        private void Start()
        {
            BindItem();

            GenerateItemView();
            
            if(itemList.Count>=1)
                SelectItem(0);
        }

        void BindItem()
        {
            int iter = 0;
            // var items = SettingManager.Singleton.ItemSetting.itemList;  // 後續應更改為當站販售物品List
            DataManager dm = DataManager.Singleton;
            var itemIds = dm.GetMapData().FindStation(DataManager.Singleton.PlayerData.currentStation).sellItemIdList;
            foreach (Transform t in itemsParent)
            {
                if (iter < itemIds.Count)
                {
                    var item = SettingManager.Singleton.ItemSetting.itemList[itemIds[iter]];
                    itemList.Add(item);
                    t.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.icon;
                    EventTrigger trigger = t.gameObject.AddComponent<EventTrigger>();
                    int temp = iter;
                    Utils.EventBinder.Bind(
                        trigger,
                        EventTriggerType.PointerClick,
                        (d) =>
                        {
                            SelectItem(temp);
                            AudioHandler.Singleton.PlaySound("select");
                        }
                    );
                    Utils.EventBinder.Bind(
                        trigger,
                        EventTriggerType.PointerEnter,
                        (d) => AudioHandler.Singleton.PlaySound("hover")
                    );
                }
                else
                {
                    t.GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
                }

                ++iter;
            }
        }

        // 生成3D物件供預覽
        void GenerateItemView()
        {
            foreach (var item in itemList)
            {
                GameObject g;
                if (item.prefab == null)
                {
                    g = new GameObject(item.itemName);
                    g.transform.parent = itemViewerParent;
                }
                else
                {
                    g = Instantiate(item.prefab, itemViewerParent);
                }
                g.transform.localPosition = Vector3.zero;
            }
        }

        void SelectItem(int index)
        {
            itemAmount = 1;

            var item = itemList[index];
            selectingItemName.text = item.itemName;
            selectingItemPrice.text = $"${item.originBuyPrice}";
            selectingItemDescription.text = item.description;

            selectingItemAmount.text = itemAmount.ToString();

            foreach (Transform t in itemViewerParent)
                t.gameObject.SetActive(false);

            itemViewerParent.GetChild(index).gameObject.SetActive(true);
        }
    }
}
