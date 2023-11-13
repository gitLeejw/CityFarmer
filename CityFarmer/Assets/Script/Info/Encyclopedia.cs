using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia 
{

    public string FoodSeq { get;  set; }
    public string FoodName { get;  set; }
    public string FoodText { get;  set; }
 
    public Time FoodTime { get;  set; }
    public int FoodPrice { get;  set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
    // TODO : 농작물 종류, 설명 작업
    public int CurrentCollectionCrops { get;  set; }
    public int MaxCollectionCrops { get;  set; }
    
}
