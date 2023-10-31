using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia 
{

    public int UserSeq { get; private set; }
    public int FoodSeq { get; private set; }
    public string FoodName { get; private set; }
    public string FoodText { get; private set; }
 
    public Time FoodTime { get; private set; }
    public int FoodPrice { get; private set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
    // TODO : 농작물 종류, 설명 작업
    public int CurrentCollectionCrops { get; private set; }
    public int MaxCollectionCrops { get; private set; }
}
