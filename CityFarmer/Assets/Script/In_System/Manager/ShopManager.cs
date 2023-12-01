using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public InventoryManager Inventory;
    public void ClickBuyButton(int shopSeq)
    {
        Shop shop = InfoManager.Instance.FindBySeq(InfoManager.Instance.Shops, shopSeq);
        Debug.Log(shop.ShopSeq);
        
        if (shop.ShopMoney)
        {
            if(InfoManager.Instance.Money.moneyRuby>= shop.ShopPrice)
            {
                ShopTypeCheck(shop);
            }
            else
            {
                Debug.Log("금액이 부족합니다.");
            }
        }
        else
        {
            if (InfoManager.Instance.Money.moneyGold >= shop.ShopPrice)
            {
                ShopTypeCheck(shop);
            }
            else
            {
                Debug.Log("금액이 부족합니다.");
            }
        }
        
          
        
          
        
      
    }
    private void ShopTypeCheck(Shop shop)
    {
        switch (shop.shopType)
        {
            case Shop.ShopType.Land: BuyLand(shop); break;
            case Shop.ShopType.Item: BuyItem(shop); break;
            case Shop.ShopType.Money: BuyMoney(shop); break;
            case Shop.ShopType.Other: BuyOther(shop); break;
        }
    }
    public void SellFood(int foodSeq, int value)
    {
        Food food = Inventory.PlayerFoodList.Find(x => x.FoodSeq == foodSeq);
        int foodIndex = Inventory.PlayerFoodList.FindIndex(x => x.FoodSeq == foodSeq);
        for (int i = 0; i < value; i++)
        {
            InfoManager.Instance.Money.moneyGold += food.FoodPrice;

        }
        food.FoodValue -= value;
        Inventory.PlayerFoodList[foodIndex] = food;

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.UserUpdateString());
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

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.MoneyUpdateString());
    }
    public void BuyLand(Shop shop)
    {
        UseMoney(shop);
        InfoManager.Instance.UserInfo.UserLandLevel++;
        InfoManager.Instance.UpdateSQL(InfoManager.Instance.UserUpdateString());

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

        InfoManager.Instance.UpdateSQL(InfoManager.Instance.MoneyUpdateString());
    }


    public void BuyOther(Shop shop)
    {
        UseMoney(shop);
    }
}
