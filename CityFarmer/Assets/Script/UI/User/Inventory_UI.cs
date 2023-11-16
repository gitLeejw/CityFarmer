using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory_UI : MonoBehaviour
{
    public InventoryManager inventory;


    public Image Itemimage;
    public TMP_Text Values;
    public TMP_Text itemname;
    public TMP_Text itemtext;
    public Button button;
    public bool Changes = false; // 농작물, 아이템 보기 바꾸기
    int Save = 0;
    void Start()
    {
    


    }
    private void OnEnable()
    {
        ShowButton();


    }
    public void ShowButton()
    {
        Transform tr = transform.GetChild(0);
        for (int i = 0; i < tr.childCount; i++)
        {
            if (inventory.PlayerFoodList.Count<i)
            {
                if (tr.GetChild(i).name.Contains("ItemButton"))
                {
                    Transform a = tr.GetChild(i);
                    a.GetComponent<Image>().sprite = inventory.PlayerFoodList[i].FoodSprite;
                    a.GetComponent<Text>().text = inventory.PlayerFoodValueList[i].ToString();

                }

            }


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void infoClick(int Button)
    {
        if (Changes)
        {
            Save = Button;
            Itemimage.sprite = inventory.PlayerItemList[Button].ItemSprite;
            itemtext.text = inventory.PlayerItemList[Button].ItemText;
            itemname.text = inventory.PlayerItemList[Button].ItemName;
        }
        else
        {
            Save = Button;
            Itemimage.sprite = inventory.PlayerItemList[Button].ItemSprite;
            itemtext.text = inventory.PlayerItemList[Button].ItemText;
            itemname.text = inventory.PlayerItemList[Button].ItemName;
        }
        
    }
}
