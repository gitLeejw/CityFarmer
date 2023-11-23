using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using UnityEngine;
public class NodeManager : MonoBehaviour
{
    public List<Nodes> Nodes = new List<Nodes>();
    public Nodes ClickNodes;
    private Mongo _mongoDB;


    void Awake()
    {
        GameObject _gameObject = InfoManager.Instance.gameObject;

        _mongoDB = _gameObject.GetComponent<Mongo>();
        _mongoDB.MongoDBConnection();

        for (int i = 0; i < _mongoDB.LoadMongo("Node").Count; i++)
        {
            BsonDocument bson = _mongoDB.LoadMongo("Node")[i];
            Nodes nodes = BsonSerializer.Deserialize<Nodes>(bson);
            Nodes.Add(nodes);
        }
    }
    public void nodeClick(int LandSeq)
    {

        if (LandSeq < _mongoDB.LoadMongo("Node").Count)
        {
            BsonDocument bson = _mongoDB.LoadMongo("Node")[LandSeq];
            Nodes nodes = BsonSerializer.Deserialize<Nodes>(bson);
            ClickNodes = nodes;
        }

    }
}
