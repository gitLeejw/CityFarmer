using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food 
{
    public int FoodSeq { get;  set; }
    public string FoodName { get;  set; }
    public string FoodText { get;  set; }
    public int FoodLevel { get;  set; }
    public Time FoodTime { get;  set; }
    public int FoodPrice { get;  set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
}
