using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    public List<Shop> LandShop;
    public List<Shop> ItemShop;
    public List<Shop> MoneyShop;
    public List<Shop> OtherShop;
    private void Start()
    {
        for (int shopIndex = 0; shopIndex < InfoManager.Instance.Shops.Count; shopIndex++)
        {
            Shop shop = InfoManager.Instance.Shops[shopIndex];
            switch (shop.shopType)
            {
                case Shop.ShopType.Land: LandShop.Add(shop); break;
                case Shop.ShopType.Item: ItemShop.Add(shop); break;
                case Shop.ShopType.Money: MoneyShop.Add(shop); break;
                case Shop.ShopType.Other: OtherShop.Add(shop); break;
            }
        }
    }
}
