using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public object _id;
    public int UserSeq;
    public List<int> ItemSeqs;

    public List<int> FoodSeqs;

    public List<int> itemValues;
    public List<int> FoodValues;
}