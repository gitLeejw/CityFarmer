using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Land
{
    public int LandSeq;
    public int UserSeq;
    public int FoodSeq;
    public string LandTime;
    public int timer;
    public Sprite LandSprite;
    public enum LandState
    {
        Harvesting,
        Cultivating,
        None

    }
    public LandState landState { get; set; }
}
public class NodeManager : MonoBehaviour
{
    public UserInfo UserInfo;
    public Tilemap Tilemap;

    public Tile LandTile;
    public Tile TransTile;
    public Grid grid;
    public List<TileBase> tilesInArea;
    public List<Vector3Int> Vector3MinPostion;
    public List<Vector3Int> Vector3MaxPostion;
    private Tile[] tileState;
    public List<Land> Lands;
    private void Awake()
    {
        // 재배 가능 영역 불러오기
        for (int i = 0; i < 8; i++)
        {
            Vector3Int minPosition = new Vector3Int(-8 + (i * 4), -3, 0);
            Vector3Int maxPosition = new Vector3Int(-6 + (i * 4), -1, 0);
            Vector3MinPostion.Add(minPosition);
            Vector3MaxPostion.Add(maxPosition);
        }


    }
    void Start()
    {

        tileState = Resources.LoadAll<Tile>("Sprite/Food");
        LoadLand();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLand()
    {
        try
        {
            Maria.SqlConnection = new MySqlConnection(Maria.strConnection);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        string query = "SELECT * FROM LAND WHERE USER_SEQ = " + UserInfo.UserSeq;
        DataSet dataSet = Maria.OnSelectRequest(query, "LAND");

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(dataSet.GetXml());



        if (xmlDocument != null)
        {


            XmlNodeList data = xmlDocument.SelectNodes("NewDataSet/LAND");

            if (data != null)
            {
                foreach (XmlNode node in data)
                {

                    Land land = new Land();
                    land.FoodSeq = System.Convert.ToInt32(node.SelectSingleNode("FOOD_SEQ").InnerText);
                    land.UserSeq = System.Convert.ToInt32(node.SelectSingleNode("USER_SEQ").InnerText);
                    land.LandSeq = System.Convert.ToInt32(node.SelectSingleNode("LAND_SEQ").InnerText);
                    string time = node.SelectSingleNode("LAND_TIME").InnerText;
                    land.LandTime = ConvertTimeString(time);
                    land.timer = ConvertTimer(time);
                    string type = node.SelectSingleNode("LAND_STATE").InnerText;

                    switch (type)
                    {
                        case "Cultivating": land.landState = Land.LandState.Cultivating; break;
                        case "Harvesting": land.landState = Land.LandState.Harvesting; break;
                        case "None": land.landState = Land.LandState.None; break;

                    }

                    Lands.Add(land);
                }


            }


        }

        Maria.SqlConnection.Close();
        for (int currentland = 0; currentland < Lands.Count; currentland++)
        {
            coordinate(currentland);
        }


    }
    private void coordinate(int land)
    {
        Vector3Int minPosition = Vector3MinPostion[land]; // 특정 영역의 최소 좌표
        Vector3Int maxPosition = Vector3MaxPostion[land]; // 특정 영역의 최대 좌표
        for (int x = minPosition.x; x <= maxPosition.x; x++)
        {

            for (int y = minPosition.y; y <= maxPosition.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                Tilemap.SetTile(tilePosition, TransTile); // 타일 변경

            }
        }
    }
    public int ConvertTimer(string inputTime)
    {
        // "PT3H40M20S" 형식의 문자열을 TimeSpan으로 파싱
        TimeSpan timeSpan = XmlConvert.ToTimeSpan(inputTime);

        // 시간 형식 "3:40:20"으로 변환
        int timer = (int)timeSpan.TotalHours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds;

        return timer;
    }
    public string ConvertTimeString(string inputTime)
    {
        // "PT3H40M20S" 형식의 문자열을 TimeSpan으로 파싱
        TimeSpan timeSpan = XmlConvert.ToTimeSpan(inputTime);

        // 시간 형식 "3:40:20"으로 변환
        string formattedTime = $"{(int)timeSpan.TotalHours}:{timeSpan.Minutes}:{timeSpan.Seconds}";

        return formattedTime;
    }
    public void LeveUPChangeLandTile(int LandLevel)
    {
        coordinate(LandLevel - 1);
        string query = "INSERT INTO LAND ( USER_SEQ, FOOD_SEQ, LAND_TIME, LAND_STATE )VALUES('" + UserInfo.UserSeq + "', '0', '00:00:00','None')";
        Maria.OnInsertOrUpdateRequest(query);
    }
    public void SaveAllLand()
    {
        for (int landNumber = 0; landNumber < Lands.Count; landNumber++)
        {
            string query = "UPDATE LAND SET FOOD_SEQ = '" + Lands[landNumber].FoodSeq + "', LAND_TIME='" + Lands[landNumber].LandTime + "',LAND_STATE = '" + Lands[landNumber].landState + "'  WHERE LAND_SEQ = '" + Lands[landNumber].LandSeq + "'";
            Maria.OnInsertOrUpdateRequest(query);
        }
    }
    public void SaveLand(int landNumber)
    {

        string query = "UPDATE LAND SET FOOD_SEQ = '" + Lands[landNumber].FoodSeq + "', LAND_TIME='" + Lands[landNumber].LandTime + "',LAND_STATE = '" + Lands[landNumber].landState + "'  WHERE LAND_SEQ = '" + Lands[landNumber].LandSeq + "'";
        Maria.OnInsertOrUpdateRequest(query);
    }
}
