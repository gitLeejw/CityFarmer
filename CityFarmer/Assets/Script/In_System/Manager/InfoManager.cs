using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Xml;

using System.Linq;

using System.Text.RegularExpressions;

public class InfoManager : MonoBehaviour
{
    public string InfoUserId = "kos1515";
    public string InfoUserPassword = "dnflskfk1";
    public UserInfo UserInfo;
    private void Awake()
    {
        try
        {
            Maria.SqlConnection = new MySqlConnection(Maria.strConnection);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }   
        //·Î±×ÀÎ Äõ¸®
        string query = "SELECT * FROM USER WHERE USER_ID = '" + InfoUserId + "' AND USER_PASSWORD = '" + InfoUserPassword+"'";
        DataSet dataSet = Maria.OnSelectRequest(query, "USER");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(dataSet.GetXml());

        if (xmlDocument != null)
        {
            XmlNodeList user = xmlDocument.SelectNodes("NewDataSet/USER");
            foreach(XmlNode node in user)
            {
                UserInfo.UserSeq =System.Convert.ToInt32( node.SelectSingleNode("USER_SEQ").InnerText);
                UserInfo.UserLevel = System.Convert.ToInt32(node.SelectSingleNode("USER_LEVEL").InnerText);
                UserInfo.UserId = node.SelectSingleNode("USER_ID").InnerText;
                UserInfo.UserName = node.SelectSingleNode("USER_NAME").InnerText;
                UserInfo.UserReg = node.SelectSingleNode("USER_REG").InnerText;
            }
            
        }
    }

	

}
