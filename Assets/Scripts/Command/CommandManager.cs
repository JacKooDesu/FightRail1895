using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBL.Command
{
    public class CommandManager : MonoBehaviour
    {
        public static Command<int> ADD_PLAYER_MONEY;
        public static Command<int, float> ITEM_BUY_PRICE_MULTIPLY;
        public static Command<int, float> ITEM_SELL_PRICE_MULTIPLY;
        public static List<object> commandList;

        void Awake()
        {
            ADD_PLAYER_MONEY = new Command<int>("addMoney", (v1) =>
            {
                
            });

            ITEM_BUY_PRICE_MULTIPLY = new Command<int, float>("itemBuyPrice", (id, amount) =>
            {
                DataManager.Singleton.ItemPriceData.BuyPriceMultiply(id, amount);
            });

            ITEM_SELL_PRICE_MULTIPLY = new Command<int, float>("itemSellPrice", (id, amount) =>
            {
                DataManager.Singleton.ItemPriceData.SellPriceMultiply(id, amount);
            });

            commandList = new List<object>{
                ADD_PLAYER_MONEY,
                ITEM_BUY_PRICE_MULTIPLY
            };

        }

        public static void CheckCommand(string s)
        {
            // print(s);
            string[] properties = s.Split(' ');

            for (int i = 0; i < commandList.Count; ++i)
            {
                CommandBase commandBase = commandList[i] as CommandBase;

                if (s.Contains(commandBase.CommandId))
                {

                    if (commandList[i] as Command != null)
                    {
                        (commandBase as Command).Invoke();
                    }
                    else if (commandList[i] as Command<int> != null)
                    {
                        (commandBase as Command<int>).Invoke(int.Parse(properties[1]));
                    }
                    else if (commandList[i] as Command<int, int> != null)
                    {
                        (commandBase as Command<int, int>).Invoke(int.Parse(properties[1]), int.Parse(properties[2]));
                    }
                }
            }
        }
    }

}
