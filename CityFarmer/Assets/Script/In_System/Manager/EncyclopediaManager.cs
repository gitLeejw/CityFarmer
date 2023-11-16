using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
public class EncyclopediaManager : MonoBehaviour
{
    public Mongo MongoDB;
    public Encyclopedia Encyclopedia;
    // Start is called before the first frame update
    void Start()
    {
        MongoDB.MongoDBConnection();

        BsonDocument bson = MongoDB.LoadMongo("Encyclopedia")[0];
        Encyclopedia encyclopedia = BsonSerializer.Deserialize<Encyclopedia>(bson);



        Encyclopedia.UserSeq = encyclopedia.UserSeq;
        Encyclopedia.FoodSeqs = encyclopedia.FoodSeqs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
