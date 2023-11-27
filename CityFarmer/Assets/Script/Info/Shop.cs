using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class Shop
{
    // Start is called before the first frame update
    public int ShopSeq { get; set; }
    public string ShopText { get; set; }
    public int ShopPrice { get; set; }
    public int ShopLevel { get; set; }
    public enum ShopType
    {
        Land,
        Money,
        Item,
        Other
    }
    public ShopType shopType { get; set; }

    public bool ShopMoney { get; set; }

    public string ShopSpriteString { get; set; }
    public Sprite ShopSprite { get; set; }

    public Sprite shopSprite()
    {
        return Resources.Load<Sprite>(ShopSpriteString);
    }

    public int ShopValue { get; set; }
}

