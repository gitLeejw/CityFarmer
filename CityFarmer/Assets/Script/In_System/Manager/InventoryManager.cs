using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Linq;
using System.Text.RegularExpressions;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;
    private Mongo _mongoDB;
    
    public List<Item> PlayerItemList;
    public List<int> PlayerItemValueList;
    public List<Food> PlayerFoodList;
    public List<int> PlayerFoodValueList;
    
    void Awake()
    {
        GameObject _gameObject = InfoManager.Instance.gameObject;
        _inventory = _gameObject.GetComponent<Inventory>();
        _mongoDB = _gameObject.GetComponent<Mongo>();
        _mongoDB.MongoDBConnection();
        LoadInventory();
        
    }

    public void LoadInventory()
    {
        Debug.Log("Load Inventory Start");
        _inventory.UserSeq = UserInfo.UserSeq;
        if (_mongoDB.LoadMongo("Inventory").Count >0)
        {
           
            BsonDocument bson = _mongoDB.LoadMongo("Inventory")[0];
            
            Inventory inventory = BsonSerializer.Deserialize<Inventory>(bson);
            _inventory.UserSeq = inventory.UserSeq;
            _inventory.FoodSeqs = inventory.FoodSeqs;
            _inventory.ItemSeqs = inventory.ItemSeqs;
            _inventory.FoodValues = inventory.FoodValues;
            _inventory.ItemValues = inventory.ItemValues;
            List<Item> items = InfoManager.Instance.Items;
            List<Food> foods = InfoManager.Instance.Foods;

            for (int inventoryIndex = 0; inventoryIndex < _inventory.ItemSeqs.Count; inventoryIndex++)
            {

                Debug.Log(_inventory.ItemSeqs[inventoryIndex]);
                Item item = FindBySeq(items, _inventory.ItemSeqs[inventoryIndex]);
                PlayerItemList.Add(item);
                PlayerItemValueList.Add(_inventory.ItemValues[inventoryIndex]);
            }

            for (int inventoryIndex = 0; inventoryIndex < _inventory.FoodSeqs.Count; inventoryIndex++)
            {
                Food food = FindBySeq(foods, _inventory.FoodSeqs[inventoryIndex]);

                PlayerFoodList.Add(food);
                PlayerFoodValueList.Add(_inventory.FoodValues[inventoryIndex]);
            }
        }
        else
        {
            Mongo.InitMongoInventory(_inventory);
        }
       
    }
    public T FindBySeq<T>(List<T> TList,int Seq)
    {
        // LINQ를 사용하여 itemSeq와 일치하는 Item 찾기
        T found = TList.FirstOrDefault(t => TypeSeq(t) == Seq);
        return found;
    }
    public int TypeSeq<T>(T t)
    {
       
        string type = t.ToJson().Split("Name")[0];
        string typeSeq = "";

        for (int i = 0; i < type.Length; i++)
        {
            char ch = type[i];
            if ('0' <= ch && ch <= '9')
            {
                typeSeq += ch;
            }
        }
        return int.Parse(typeSeq);
    }

}
