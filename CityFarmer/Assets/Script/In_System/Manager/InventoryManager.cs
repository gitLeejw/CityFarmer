using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
public class InventoryManager : MonoBehaviour
{
    public Inventory Inventory;
    public Mongo MongoDB;
    void Start()
    {
        MongoDB.MongoDBConnection();

        BsonDocument bson = MongoDB.LoadMongo("Inventory")[0];
        Inventory inventory = BsonSerializer.Deserialize<Inventory>(bson);
        Inventory.UserSeq = inventory.UserSeq;
        Inventory.FoodSeqs = inventory.FoodSeqs;
        Inventory.ItemSeqs = inventory.ItemSeqs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
