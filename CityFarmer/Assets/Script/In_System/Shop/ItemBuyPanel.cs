using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemBuyPanel : MonoBehaviour
{
    public Shop_UI Shop_UI;
    public ShopData ShopData;
    private void Start()
    {
        Shop_UI.CreateButton(ShopData.ItemShop,transform);
    }

    
}
