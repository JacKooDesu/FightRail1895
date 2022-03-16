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
        public Text selectingItemDescription;

        [Header("按鈕")]
        public EventTrigger addAmountButton;
        public EventTrigger subAmountButton;

        [Header("3D預覽")]
        public Transform itemViewerParent;

        public List<Item.ItemSetting> itemList = new List<Item.ItemSetting>();

        public bool isSelling;  // true=>正在買東西 || false=>正在賣東西

        public Button sellButton, buyButton;

        private void Start()
        {
            isSelling = true;
            BindEvent();
            BindItem();

            GenerateItemView();

            if (itemList.Count >= 1)
                SelectItem(0);

            sellButton.onClick.Invoke();
        }

        void BindEvent()
        {
            int iter = 0;
            foreach (Transform t in itemsParent)
            {
                EventTrigger trigger = t.gameObject.AddComponent<EventTrigger>();
                int temp = iter;
                Utils.EventBinder.Bind(
                    trigger,
                    EventTriggerType.PointerClick,
                    (d) =>
                    {
                        if (temp < itemList.Count)
                        {
                            SelectItem(temp);
                            AudioHandler.Singleton.PlaySound("select");
                        }
                    }
                );
                Utils.EventBinder.Bind(
                    trigger,
                    EventTriggerType.PointerEnter,
                    (d) =>
                    {
                        if (temp < itemList.Count)
                            AudioHandler.Singleton.PlaySound("hover");
                    }
                );

                ++iter;
            }

            sellButton.onClick.AddListener(() =>
            {
                sellButton.transform.Find("Cover").gameObject.SetActive(false);
                buyButton.transform.Find("Cover").gameObject.SetActive(true);
                isSelling = true;     // 0號是正在買東西
                BindItem();
                GenerateItemView();

                if (itemList.Count >= 1)
                    SelectItem(0);
            });

            buyButton.onClick.AddListener(() =>
            {
                buyButton.transform.Find("Cover").gameObject.SetActive(false);
                sellButton.transform.Find("Cover").gameObject.SetActive(true);
                isSelling = false;     // 0號是正在買東西
                BindItem();
                GenerateItemView();

                if (itemList.Count >= 1)
                    SelectItem(0);
            });
        }

        void BindItem()
        {
            itemList = new List<Item.ItemSetting>();

            // var items = SettingManager.Singleton.ItemSetting.itemList;  // 後續應更改為當站販售物品List
            DataManager dm = DataManager.Singleton;
            var itemIds = isSelling ?
                dm.GetMapData().FindStation(DataManager.Singleton.PlayerData.currentStation).sellItemIdList :
                dm.GetMapData().FindStation(DataManager.Singleton.PlayerData.currentStation).buyItemIdList;

            int iter = 0;
            foreach (Transform t in itemsParent)
            {
                if (iter < itemIds.Count)
                {
                    var item = SettingManager.Singleton.ItemSetting.itemList[itemIds[iter]];
                    itemList.Add(item);
                    t.Find("icon").GetComponent<Image>().sprite = item.icon;
                    // t.Find("Cover").gameObject.SetActive(false);
                }
                else
                {
                    t.Find("icon").GetComponent<Image>().sprite = null;
                    // t.Find("Cover").gameObject.SetActive(true);
                }

                ++iter;
            }
        }

        // 生成3D物件供預覽
        void GenerateItemView()
        {
            foreach (Transform child in itemViewerParent)
                Destroy(child.gameObject);

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
            var item = itemList[index];
            selectingItemName.text = item.itemName;
            selectingItemDescription.text = item.description;

            foreach (Transform t in itemViewerParent)
                t.gameObject.SetActive(false);

            itemViewerParent.GetChild(index).gameObject.SetActive(true);
        }
    }
}
