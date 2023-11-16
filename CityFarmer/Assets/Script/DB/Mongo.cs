using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Reflection;

public class Mongo : MonoBehaviour
{
    public static MongoClient client;
   public static IMongoDatabase database;
   
    void Start()
    {


  
        

    }
    public  void MongoDBConnection()
    {
        string connectionString = "mongodb+srv://myUser:dnflskfk1@mydb.qgumh09.mongodb.net/MyDB?retryWrites=true&appName=AtlasApp";

        // MongoClient 생성
        client = new MongoClient(connectionString);

        // 데이터베이스 선택
        database = client.GetDatabase("MyDB");

        
    }

    // 여기에서 쿼리 실행 등 MongoDB 작업 수행
    public  List<BsonDocument> LoadMongo(string DB)
    {
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(DB);

        var filter = Builders<BsonDocument>.Filter.Eq("UserSeq", UserInfo.UserSeq);
        List<BsonDocument> result = collection.Find(filter).ToList();

        return result;

    }
  
    public  void InitMongo<T>(string DB,T type)
    {
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(DB);


        
        

        string json = JsonUtility.ToJson(type);
        BsonDocument bson = JsonToBson(json);
       
        // 데이터 저장
        collection.InsertOne(bson);

        Debug.Log("Data inserted successfully!");
    }
    public void UpdateMongo<T>(Inventory inventory,Encyclopedia encyclopedia,T type)
    {
      


        

        string typetext = type.GetType().ToString();
       
        Debug.Log(UserInfo.UserSeq);
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(typetext);
        var filter = Builders<BsonDocument>.Filter.Eq("UserSeq", UserInfo.UserSeq);
        switch (typetext)
        {
            case "Inventory":
                
                var InventoryItemupdate = Builders<BsonDocument>.Update.Set("ItemSeqs", inventory.ItemSeqs);
                var InventoryFoodupdate = Builders<BsonDocument>.Update.Set("FoodSeqs", inventory.FoodSeqs);
                // 데이터 저장
                var inventoryItemResult = collection.UpdateOne(filter, InventoryItemupdate);
                var inventoryFoodResult = collection.UpdateOne(filter, InventoryFoodupdate); break;
            case "Encyclopedia":
                var EncyclopediaFoodupdate = Builders<BsonDocument>.Update.Set("FoodSeqs", encyclopedia.FoodSeqs);            
                var EncyclopediaFoodresult = collection.UpdateOne(filter, EncyclopediaFoodupdate);
                break;
           
        }
       
   
        Debug.Log("Data inserted successfully!");
    }
    public  BsonDocument JsonToBson(string jsonString)
    {
        // JSON 문자열을 BsonDocument로 변환
        using (JsonReader reader = new JsonReader(jsonString))
        {
            BsonDocument bsonDocument = BsonSerializer.Deserialize<BsonDocument>(reader);
            return bsonDocument;
        }
    }
}
