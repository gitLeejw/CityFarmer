using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSellPanel : MonoBehaviour
{
    public Inventory_UI Inventory_UI;

    private void Start()
    {
        Inventory_UI.ShowButtonFood(transform);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
