using System;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Data
{
    [Serializable]
    public class ItemPriceData
    {
        [Serializable]
        public class PriceSetting
        {
            public int id;
            public float buyPrice;
            public float sellPrice;
            public PriceSetting(Item.TradeItem item)
            {
                this.id = item.id;
                this.buyPrice = item.originBuyPrice;
                this.sellPrice = item.originSellPrice;
            }
        }

        public List<PriceSetting> priceSettings = new List<PriceSetting>();

        public void BuyPriceMultiply(int id, float multiply)
        {
            int x = priceSettings.FindIndex((i) => i.id == id);
            if (x != -1)
            {
                priceSettings[x].buyPrice *= multiply;
            }
        }

        public void SellPriceMultiply(int id, float multiply)
        {
            int x = priceSettings.FindIndex((i) => i.id == id);
            if (x != -1)
            {
                priceSettings[x].sellPrice *= multiply;
            }
        }

        // public void RandPrice(float )
        // {

        // }

        public void Init()
        {
            foreach (Item.TradeItem item in SettingManager.Singleton.ItemSetting.tradeItemList)
            {
                priceSettings.Add(new PriceSetting(item));

            }
        }
    }
}