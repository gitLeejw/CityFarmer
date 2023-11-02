using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
public class Maria : MonoBehaviour
{
    public static MySqlConnection SqlConnection;

    static string _ipAddress = "13.209.143.246";
    static string _dbId = "poly";
    static string _dbPw = "Poly01!";
    static string _dbName = "myDB";

    string strConnection = string.Format("server={0};uid={1};pwd={2};database={3};charset=utf8 ;", _ipAddress, _dbId, _dbPw, _dbName);
    private void Awake()
    {
        try
        {
            SqlConnection = new MySqlConnection(strConnection);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    void Start()
    {
        string query = "SELECT * FROM USER";
        DataSet dataSet = OnSelectRequest(query, "USER");
        Debug.Log(dataSet.GetXml());
        
    }
    //데이터 , 삽입, 업데이트 쿼리 함수
    public static bool OnInsertOrUpdateRequest(string str_query)
    {
        try
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = str_query;

            SqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return true;
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }
    // 데이터 조회시 사용
    public static DataSet OnSelectRequest(string p_query,string table_name)
    {
        try
        {
            SqlConnection.Open();
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = p_query;
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, table_name);
            SqlConnection.Close();

            return dataSet;
        }catch(System.Exception e)
        {
            Debug.Log(e.ToString());

            return null;
        }
    }
    private void OnApplicationQuit()
    {
        SqlConnection.Close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
