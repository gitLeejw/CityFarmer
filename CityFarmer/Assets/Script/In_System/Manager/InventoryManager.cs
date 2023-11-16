using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public Inventory Inventory;
    public Mongo MongoDB;
    public InfoManager InfoManager;
    public List<Item> PlayerItemList;
    public List<int> PlayerItemValueList;
    public List<Food> PlayerFoodList;
    public List<int> PlayerFoodValueList;
    void Start()
    {

        MongoDB.MongoDBConnection();
        LoadInventory();



    }
    public void LoadInventory()
    {
        BsonDocument bson = MongoDB.LoadMongo("Inventory")[0];
        Inventory inventory = BsonSerializer.Deserialize<Inventory>(bson);
        Inventory.UserSeq = inventory.UserSeq;
        Inventory.FoodSeqs = inventory.FoodSeqs;
        Inventory.ItemSeqs = inventory.ItemSeqs;
        Inventory.FoodValues = inventory.FoodValues;
        Inventory.ItemValues = inventory.ItemValues;
        List<Item> items = InfoManager.Items;
        List<Food> foods = InfoManager.Foods;
        for (int inventoryIndex =0; inventoryIndex< Inventory.ItemSeqs.Count; inventoryIndex++)
        {
            Item item = FindItemBySeq(items, Inventory.ItemSeqs[inventoryIndex]);
            PlayerItemList.Add(item);
            PlayerItemValueList.Add(Inventory.ItemValues[inventoryIndex]);
        }
        for (int inventoryIndex = 0; inventoryIndex < Inventory.FoodSeqs.Count; inventoryIndex++)
        {
            Food food = FindFoodBySeq(foods, Inventory.FoodSeqs[inventoryIndex]);
            PlayerFoodList.Add(food);
            PlayerFoodValueList.Add(Inventory.FoodValues[inventoryIndex]);
        }

    }
    public Item FindItemBySeq(List<Item> itemList,int itemSeq)
    {
        // LINQ를 사용하여 itemSeq와 일치하는 Item 찾기
        Item foundItem = itemList.FirstOrDefault(item => item.ItemSeq == itemSeq);

        return foundItem;
    }
    public Food FindFoodBySeq(List<Food> foodList, int foodSeq)
    {
        // LINQ를 사용하여 itemSeq와 일치하는 Item 찾기
        Food foundfood = foodList.FirstOrDefault(food => food.FoodSeq == foodSeq);

        return foundfood;
    }
    // Update is called once per frame

}
