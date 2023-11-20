using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Inventory_UI : MonoBehaviour
{
    public InventoryManager Inventory;
    public GameObject ItemPanel;
    public Image Itemimage;
    public TMP_Text Values;
    public TMP_Text Itemname;
    public TMP_Text Itemtext;
    public Button  Button;
    public bool Changes = false; // 농작물, 아이템 보기 바꾸기
   
    
    private void OnEnable()
    {
        ShowButton();
    }
    public void ShowButton()
    {
        Transform panel = transform.GetChild(0);
      
        for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
        {
            if (Inventory.PlayerFoodList.Count>childIndex)
            {
                if (panel.GetChild(childIndex).name.Contains("ItemButton"))
                {
                    Transform button = panel.GetChild(childIndex);
                    Debug.Log(button.name);
                    button.GetComponent<Image>().sprite = Inventory.PlayerFoodList[childIndex].FoodSprite;
                    button.GetComponentInChildren<TMP_Text>().text = Inventory.PlayerFoodValueList[childIndex].ToString();
                }
            }
            else
            {
                break;
            }
        }
    }
    public void InfoFoodClick()
    {
        int ButtonIndex = 0;
        GameObject _gameObject = EventSystem.current.currentSelectedGameObject;
        Transform panel = transform.GetChild(0);

        for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
        {
           
           if (panel.GetChild(childIndex).name.Equals(_gameObject.name))
           {
               ButtonIndex = childIndex;
                Debug.Log(ButtonIndex);
           }
          
        }
        Itemimage.sprite = Inventory.PlayerFoodList[ButtonIndex].FoodSprite;
        Itemname.text = Inventory.PlayerFoodList[ButtonIndex].FoodName;
        Itemtext.text = Inventory.PlayerFoodList[ButtonIndex].FoodText;
        Values.text = Inventory.PlayerFoodValueList[ButtonIndex].ToString();
        gameObject.SetActive(false);
        ItemPanel.SetActive(true);
    }
    public void InfoItemClick(int Button)
    {
        
        Itemimage.sprite = Inventory.PlayerFoodList[Button].FoodSprite;
        Itemname.text = Inventory.PlayerFoodList[Button].FoodName;
        Itemtext.text = Inventory.PlayerFoodList[Button].FoodText;
        Values.text = Inventory.PlayerFoodValueList[Button].ToString();
        gameObject.SetActive(false);
        ItemPanel.SetActive(true);
    }
}
