using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food 
{
    public int FoodSeq { get; private set; }
    public string FoodName { get; private set; }
    public string FoodText { get; private set; }
    public int FoodLevel { get; private set; }
    public Time FoodTime { get; private set; }
    public int FoodPrice { get; private set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
}
