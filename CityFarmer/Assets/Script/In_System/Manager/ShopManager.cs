using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Linq;
using System.Xml;

public class ShopManager : MonoBehaviour
{
   
    public InventoryManager Inventory;
    
    public void SellFood(int foodSeq, int value)
    {
        Food food  = Inventory.PlayerFoodList.Find(x => x.FoodSeq == foodSeq);
        int foodIndex = Inventory.PlayerFoodList.FindIndex(x => x.FoodSeq == foodSeq);
        for (int i =0; i<value; i++)
        {
            InfoManager.Instance.Money.moneyGold += food.FoodPrice;
            
        }
        food.FoodValue -= value;
        Inventory.PlayerFoodList[foodIndex] = food;

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.MoneyUpdateQuery);
    }
    public void BuyItem(Shop shop)
    {
        UseMoney(shop);
        Item item = Inventory.PlayerItemList.Find(x => x.ItemSeq == shop.ItemSeq);
        int itemIndex = Inventory.PlayerItemList.FindIndex(x => x.ItemSeq == shop.ItemSeq);
        if (item == null)
        {
            item = InfoManager.Instance.FindBySeq(InfoManager.Instance.Items, shop.ItemSeq);
            item.ItemValue = shop.ShopValue;
        }
        else
        {
            item.ItemValue += shop.ShopValue;
            Inventory.PlayerItemList.RemoveAt(itemIndex);
        }
        Inventory.PlayerItemList.Add(item);

       
    }
    public void BuyMoney(Shop shop)
    {
        UseMoney(shop);
        if (!shop.ShopMoney)
        {
            InfoManager.Instance.Money.moneyRuby += shop.ShopValue;
        }
        else
        {
            InfoManager.Instance.Money.moneyGold += shop.ShopValue;
        }

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.MoneyUpdateQuery);
    }
    public void BuyLand(Shop shop)
    {
        UseMoney(shop);
        InfoManager.Instance.UserInfo.UserLandLevel++;
        InfoManager.Instance.UpdateSQL(InfoManager.Instance.UserUpdateQuery);

    }
    public void UseMoney(Shop shop)
    {
        if (shop.ShopMoney)
        {
            InfoManager.Instance.Money.moneyRuby -= shop.ShopPrice;
        }
        else
        {
            InfoManager.Instance.Money.moneyGold -= shop.ShopPrice;
        }

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.MoneyUpdateQuery);
    }
  
   
    public void BuyOther(Shop shop)
    {
        UseMoney(shop);
    }
}
