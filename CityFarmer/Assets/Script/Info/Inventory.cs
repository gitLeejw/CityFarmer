using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public List<int> ItemSeqs { get;  set; }
    public string ItemName { get;  set; }
    public string ItemText { get;  set; }
    public int ItemPrice { get;  set; }
    public enum Itemtype
    {
        disposable,
        costume
    }


}
