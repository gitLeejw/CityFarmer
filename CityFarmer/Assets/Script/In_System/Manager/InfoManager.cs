using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Xml;


[Serializable]
public class Item
{
    public int ItemSeq { get; set; }
    public string ItemName { get; set; }
    public string ItemText { get; set; }
    public int ItemPrice { get; set; }
    public enum Itemtype
    {
        disposable,
        costume
    }
    public Itemtype itemtype { get; set; }

    public bool ItemMoney { get; set; }

    public string ItemSpriteString { get; set; }
    public Sprite ItemSprite;

    public Sprite itemSprite()
    {
        return Resources.Load<Sprite>(ItemSpriteString);
    }
}
[Serializable]
public class Food 
{
    public int FoodSeq { get; set; }
    public string FoodName { get; set; }
    public string FoodText { get; set; }
    public int FoodLevel { get; set; }
    public string FoodTime { get; set; }
    public int FoodPrice { get; set; }
    public enum Foodtype
    {
        Plant,
        Meat
    }
    public Foodtype foodtype { get; set; }
    public string FoodSpriteString { get; set; }
    public Sprite FoodSprite { get; set; }

    public Sprite foodSprite()
    {
        return Resources.Load<Sprite>(FoodSpriteString);
    }
}

public class InfoManager : MonoBehaviour
{
   
    public UserInfo UserInfo;
    public Money Money;
    
    public List<Item> Items = new List<Item>();
    public Land Land;

    public List<Food> Foods = new List<Food>();
    
   
    private void Awake()
    {
        
        Login("kos1515", "dnflskfk1");
      
        Items = new List<Item>();
        Foods = new List<Food>();
       
        _loadFood();
        _loadItem();
       


     




    }
    private void Start()
    {
      
         
     
    }
    private XmlDocument OnLoadData(string DB)
    {

        startSQL();
        string query = "SELECT * FROM " + DB;
        DataSet dataSet = Maria.OnSelectRequest(query, DB);

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(dataSet.GetXml());
        return xmlDocument;
    }
    private void startSQL()
    {
        try
        {
            Maria.SqlConnection = new MySqlConnection(Maria.strConnection);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

    }
    private void _loadFood()
    {
        XmlDocument xmlDocument = OnLoadData("FOOD");
    
      

        if (xmlDocument != null) {

            
            XmlNodeList data = xmlDocument.SelectNodes("NewDataSet/FOOD");

            int foodCount = 0;
            foreach (XmlNode node in data)
            {
                foodCount++;
                Food food = new Food();
                food.FoodSeq = System.Convert.ToInt32(node.SelectSingleNode("FOOD_SEQ").InnerText);
                food.FoodName = node.SelectSingleNode("FOOD_NAME").InnerText;
                food.FoodText = node.SelectSingleNode("FOOD_TEXT").InnerText;
                food.FoodPrice = System.Convert.ToInt32(node.SelectSingleNode("FOOD_PRICE").InnerText);
                string time = node.SelectSingleNode("FOOD_TIME").InnerText;
                food.FoodTime = time.Split("T")[1];
                string type =node.SelectSingleNode("FOOD_TYPE").InnerText;
                food.FoodLevel = System.Convert.ToInt32(node.SelectSingleNode("FOOD_LEVEL").InnerText);
                food.FoodSpriteString = node.SelectSingleNode("FOOD_SPRITE").InnerText;
                food.FoodSprite = food.foodSprite();

                switch (type)
                {
                    case "Plant": food.foodtype = Food.Foodtype.Plant; break;
                    case "Meat": food.foodtype = Food.Foodtype.Meat; break;

                }
                
                Foods.Add(food);
               
            }

        }

        Maria.SqlConnection.Close();
    }
    private void _loadItem()
    {
        

        XmlDocument xmlDocument = OnLoadData("ITEM");

        if (xmlDocument != null)
        {


            XmlNodeList data = xmlDocument.SelectNodes("NewDataSet/ITEM");

            int itemCount = 0;
            foreach (XmlNode node in data)
            {
                itemCount++;
                Item item = new Item();
                item.ItemSeq = System.Convert.ToInt32(node.SelectSingleNode("ITEM_SEQ").InnerText);
                item.ItemName = node.SelectSingleNode("ITEM_NAME").InnerText;
                item.ItemText = node.SelectSingleNode("ITEM_TEXT").InnerText;
                item.ItemPrice = System.Convert.ToInt32(node.SelectSingleNode("ITEM_PRICE").InnerText);
                string type = node.SelectSingleNode("ITEM_TYPE").InnerText;
                
                item.ItemSpriteString = node.SelectSingleNode("ITEM_SPRITE").InnerText;
                item.ItemSprite = item.itemSprite();
                switch (type)
                {
                    case "disposable": item.itemtype = Item.Itemtype.disposable; break;
                    case "costume": item.itemtype = Item.Itemtype.costume; break;

                }
                item.ItemMoney = System.Convert.ToBoolean(node.SelectSingleNode("ITEM_MONEY").InnerText);
                Items.Add(item);

            }

        }

        Maria.SqlConnection.Close();
    }
    public void Login(string InfoUserId,string InfoUserPassword)
    {
        startSQL();
        //로그인 쿼리
        string query = "SELECT * FROM USER WHERE USER_ID = '" + InfoUserId + "' AND USER_PASSWORD = '" + InfoUserPassword + "'";
        DataSet dataSet = Maria.OnSelectRequest(query, "USER");
       
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(dataSet.GetXml());

        if (xmlDocument != null)
        {
            XmlNodeList user = xmlDocument.SelectNodes("NewDataSet/USER");
            foreach (XmlNode node in user)
            {
                UserInfo.UserSeq = System.Convert.ToInt32(node.SelectSingleNode("USER_SEQ").InnerText);
                UserInfo.UserLevel = System.Convert.ToInt32(node.SelectSingleNode("USER_LEVEL").InnerText);
                UserInfo.UserId = node.SelectSingleNode("USER_ID").InnerText;
                UserInfo.UserName = node.SelectSingleNode("USER_NAME").InnerText;
                UserInfo.UserReg = node.SelectSingleNode("USER_REG").InnerText;
            }

        }
      
        Maria.SqlConnection.Close();
    }
    public void SignUp(string InfoUserId, string InfoUserPassword,string InfoUserName){
        startSQL();
        string query = "SELECT * FROM USER WHERE USER_ID = '" + InfoUserId + "'";
        DataSet dataSet = Maria.OnSelectRequest(query, "USER");
        if (dataSet.GetXml().IndexOf("SEQ") >= 0)
        {
            Debug.Log("이미 가입된 아이디입니다.");
        }
        else
        {

            //회원가입 쿼리
            query = "INSERT INTO USER ( USER_ID, USER_LEVEL, USER_NAME, USER_PASSWORD )VALUES('" + InfoUserId + "', '1', '" + InfoUserName + "','" + InfoUserPassword + "')";
            if (Maria.OnInsertOrUpdateRequest(query))
            {
                Debug.Log("회원가입에 성공하였습니다.");
            }
            else
            {
                Debug.Log("회원가입에 실패하였습니다.");

            }
        }
   
      
    }
	

}
