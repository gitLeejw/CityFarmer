
using System;

using UnityEngine;


[Serializable]
public class Item
{
    public int ItemSeq { get; set; }
    public string ItemName { get; set; }
    public string ItemText { get; set; }
    public int ItemPrice { get; set; }
    public enum ItemType
    {
        Disposable,
        Costume
    }
    public ItemType itemType { get; set; }

    public bool ItemMoney { get; set; }

    public string ItemSpriteString { get; set; }
    public Sprite ItemSprite { get; set; }

    public Sprite itemSprite()
    {
        return Resources.Load<Sprite>(ItemSpriteString);
    }

    public int ItemValue { get; set; }
}
