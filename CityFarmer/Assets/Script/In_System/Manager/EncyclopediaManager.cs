using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    private Mongo _mongoDB;
    private Encyclopedia _encyclopedia;

    public int CurrentCollectionFoods { get; private set; }
    public int MaxCollectionFoods { get; private set; }

    private void Awake()
    {
        GameObject instance = InfoManager.Instance.gameObject;
        _encyclopedia = instance.GetComponent<Encyclopedia>();
        _mongoDB = instance.GetComponent<Mongo>();
    }

    private void Start()
    {
        InitDB();
        InitFoodCount();
    }

    private void InitDB()
    {
        _mongoDB.MongoDBConnection();

        BsonDocument bson = _mongoDB.LoadMongo("Encyclopedia")[0];
        Encyclopedia encyclopedia = BsonSerializer.Deserialize<Encyclopedia>(bson);

        _encyclopedia.UserSeq = encyclopedia.UserSeq;
        _encyclopedia.FoodSeqs = encyclopedia.FoodSeqs;
    }

    //TODO : 동기화 시 현재 수집 개수 , 최대 수집개수 초기화
    public void InitFoodCount()
    {
        CurrentCollectionFoods = _encyclopedia.FoodSeqs.Count;
        MaxCollectionFoods = InfoManager.Instance.Foods.Count;
    }
}
