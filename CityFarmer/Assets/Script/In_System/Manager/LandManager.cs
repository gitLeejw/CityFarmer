using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandManager : MonoBehaviour
{
    public Tilemap Tilemap;
    public Grid grid;
    public List<Vector3Int> Vector3MinPostion;
    public List<Vector3Int> Vector3MaxPostion;
    public Dictionary<Vector3Int, int> Vector3Node = new Dictionary<Vector3Int, int>();
    public List<Node> NodeList;

    public List<Nodes> NodesList = new List<Nodes>();
    public Nodes ClickNodes;
    private Mongo _mongoDB;
    public int LandSeq;
    public GameObject _nodePopUp;
    public bool OnNodePopUp = true;
    private void Awake()
    {
        GameObject _gameObject = InfoManager.Instance.gameObject;

        _mongoDB = _gameObject.GetComponent<Mongo>();
        _mongoDB.MongoDBConnection();

        for (int i = 0; i < _mongoDB.LoadMongo("Node").Count; i++)
        {
            BsonDocument bson = _mongoDB.LoadMongo("Node")[i];
            Nodes nodes = BsonSerializer.Deserialize<Nodes>(bson);
            NodesList.Add(nodes);
        }
        // ��� ���� ���� �ҷ�����
        for (int i = 0; i < 8; i++)
        {
            Vector3Int minPosition = new Vector3Int(-8 + (i * 4), -3, 0);
            Vector3Int maxPosition = new Vector3Int(-6 + (i * 4), -1, 0);
            Vector3MinPostion.Add(minPosition);
            Vector3MaxPostion.Add(maxPosition);
        }

    }

    private void Start()
    {
        LoadLand();
    }
    private void Update()
    {
        // ���� �����丵 ������ ���� ������Ʈ������ ����
        if (Input.GetMouseButton(0))
        {
            Vector3Int mousePos = GetMousePosition();
            Debug.Log(ASD(Input.mousePosition));
            if (Vector3Node.ContainsKey(mousePos) && OnNodePopUp)
            {
                LandSeq = Vector3Node[mousePos];
                _nodePopUp.SetActive(true);
                OnNodePopUp = false;

            }

        }
    }

    private bool ASD(Vector3 mousePosition)
    {
        Vector3 a1 = new Vector3(120, 305, 0);
        Vector3 a2 = new Vector3(538, 65, 0);

        if (mousePosition.x >= a1.x && mousePosition.y <= a1.y)
        {
            if (mousePosition.x <= a2.x && mousePosition.y >= a2.y)
            {
                return true;
            }
        }

        return false;
    }
    public Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
    public void LoadLand()
    {
        for (int currentland = 0; currentland < NodesList.Count; currentland++)
        {
            coordinate(currentland);
        }
    }
    public void SaveLand()
    {

        for (int nodeIndex = 0; nodeIndex < NodesList.Count; nodeIndex++)
        {
            Mongo.UpdateMongoNodes(NodesList[nodeIndex]);
        }

    }
    private void coordinate(int land)
    {
        Vector3Int minPosition = Vector3MinPostion[land]; // Ư�� ������ �ּ� ��ǥ
        Vector3Int maxPosition = Vector3MaxPostion[land]; // Ư�� ������ �ִ� ��ǥ
        List<Vector3Int> vector3s = new List<Vector3Int>();
        for (int x = minPosition.x; x <= maxPosition.x; x++)
        {
            for (int y = minPosition.y; y <= maxPosition.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                vector3s.Add(tilePosition);
            }
        }
        for (int tileCount = 0; tileCount < vector3s.Count; tileCount++)
        {
            Node node = new Node(vector3s[tileCount], NodesList[land].Lands[tileCount][0], NodesList[land].Lands[tileCount][1]);
            node.State = (Node.NodeState)NodesList[land].Lands[tileCount][2];
            node.SetNodeTile();
            NodeList.Add(node);
            Vector3Node.Add(node.GetPosition(), land);
            ChangeTile(node); // Ÿ�� ����
        }
    }
    public void ChangeTile(Node node)
    {
        Tilemap.SetTile(node.GetPosition(), node.GetStateNodeTile());
    }
    public int ConvertTimer(string inputTime)
    {
        // "PT3H40M20S" ������ ���ڿ��� TimeSpan���� �Ľ�
        TimeSpan timeSpan = XmlConvert.ToTimeSpan(inputTime);

        // �ð� ���� "3:40:20"���� ��ȯ
        int timer = (int)timeSpan.TotalHours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds;

        return timer;
    }
    public string ConvertString(int inputTime)
    {
        string timer = "";
        int h, m, s;
        h = inputTime / 3600;
        m = inputTime / 60 % 60;
        s = inputTime % 60;
        timer += h.ToString() + ":";
        timer += m.ToString() + ":";
        timer += s.ToString();
        return timer;
    }
    public string ConvertTimeString(string inputTime)
    {
        TimeSpan timeSpan = XmlConvert.ToTimeSpan(inputTime);
        string formattedTime = $"{(int)timeSpan.TotalHours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        return formattedTime;
    }
    public void LeveUPChangeLandTile(int LandLevel)
    {
        coordinate(LandLevel - 1);
        string query = "INSERT INTO LAND ( USER_SEQ )VALUES('" + UserInfo.UserSeq + "')";
        Maria.OnInsertOrUpdateRequest(query);
    }

}
