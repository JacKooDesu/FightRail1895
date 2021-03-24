using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUIObject : MonoBehaviour
{
    public Item item;

    public Transform itemState; // 敘述父物件
    public Text nameText;       // 名稱物件
    public Text stackText;      // 堆疊說明
    public Text descriptionText;   // 說明物件
    public Text valueText;      // 價值


    public bool isHovering = false;

    private void OnEnable()
    {
        if (nameText == null)
            nameText = itemState.Find("Name").GetComponent<Text>();

        if (stackText == null)
            stackText = itemState.Find("Stack").GetComponent<Text>();

        if (descriptionText == null)
            descriptionText = itemState.Find("Description").GetComponent<Text>();

        if (valueText == null)
            valueText = itemState.Find("Value").GetComponent<Text>();

        // 設定物件貼圖，之後改成MeshRenderer
        GetComponent<Image>().sprite = item.icon;

        // 設定物件名稱
        nameText.text = item.itemName;
        nameText.color = Item.rarityColors[(int)item.rarity];

        // 設定物件敘述
        descriptionText.text = item.description;

        // 設定堆疊敘述
        stackText.text = (item.stackable ? "可堆疊，最高 " + item.maxStack.ToString() : "不可堆疊");
        stackText.color = (item.stackable ? new Color(.22f, .86f, .27f) : new Color(.85f, .22f, .26f));

        // 設定價值
        valueText.text = "$" + item.value.ToString();

        // 綁定鼠標事件
        // 懸浮開始
        EventTrigger.Entry hoverStart = new EventTrigger.Entry();
        hoverStart.eventID = EventTriggerType.PointerEnter;
        hoverStart.callback.AddListener(delegate { isHovering = true; });

        // 懸浮結束
        EventTrigger.Entry hoverEnd = new EventTrigger.Entry();
        hoverEnd.eventID = EventTriggerType.PointerExit;
        hoverEnd.callback.AddListener(delegate { isHovering = false; });

        // 加入EventTrigger
        GetComponent<EventTrigger>().triggers.Add(hoverStart);
        GetComponent<EventTrigger>().triggers.Add(hoverEnd);
    }

    private void Update()
    {
        if (isHovering)
        {
            itemState.transform.position = Input.mousePosition;
        }
    }
}
