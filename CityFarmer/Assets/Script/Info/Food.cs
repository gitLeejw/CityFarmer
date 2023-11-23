
using System;
using UnityEngine;
[Serializable]
public class Food
{
    public int FoodSeq { get; set; }
    public string FoodName { get; set; }
    public string FoodText { get; set; }
    public int FoodLevel { get; set; }
    public string FoodTime { get; set; }
    public int FoodPrice { get; set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
    public Foodtype foodtype { get; set; }
    public string FoodSpriteString { get; set; }
    public Sprite FoodSprite { get; set; }

    public Sprite foodSprite()
    {
        return Resources.Load<Sprite>(FoodSpriteString);
    }
    public int FoodValue { get; set; }
}
