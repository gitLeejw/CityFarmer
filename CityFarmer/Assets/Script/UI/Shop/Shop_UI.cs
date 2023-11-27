using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_UI : MonoBehaviour
{
    public ShopManager ShopManager;



    public void ClickBuyButton(int shopSeq)
    {
        Shop shop = InfoManager.Instance.FindBySeq(InfoManager.Instance.Shops, shopSeq);

        switch (shop.shopType)
        {
            case Shop.ShopType.Land: ShopManager.BuyLand(shop); break;
            case Shop.ShopType.Item: ShopManager.BuyItem(shop); break;
            case Shop.ShopType.Money: ShopManager.BuyMoney(shop); break;
            case Shop.ShopType.Other: ShopManager.BuyOther(shop); break;
        }
    }
}
