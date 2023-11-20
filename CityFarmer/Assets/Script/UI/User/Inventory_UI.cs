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
     // 농작물, 아이템 보기 바꾸기
   
    
    private void OnEnable()
    {
        ShowButton();
    }
    public void ChangeButton(bool Change)
    {
        Transform panel;
        if (!Change)
        {
            panel = transform.GetChild(1);
            Debug.Log(panel.name);
            panel.gameObject.SetActive(false);
            panel = transform.GetChild(0);
        }
        else
        {
            panel = transform.GetChild(0);
            panel.gameObject.SetActive(false);
            panel = transform.GetChild(1);
        }
        panel.gameObject.SetActive(true);
            
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
        panel = transform.GetChild(1);

        for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
        {
            if (Inventory.PlayerItemList.Count > childIndex)
            {
                if (panel.GetChild(childIndex).name.Contains("ItemButton"))
                {
                    Transform button = panel.GetChild(childIndex);
                    Debug.Log(button.name);
                    
                    button.GetComponent<Image>().sprite = Inventory.PlayerItemList[childIndex].ItemSprite;
                    button.GetComponentInChildren<TMP_Text>().text = Inventory.PlayerItemValueList[childIndex].ToString();
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
    public void InfoItemClick()
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
            Itemimage.sprite = Inventory.PlayerItemList[ButtonIndex].ItemSprite;
            Itemname.text = Inventory.PlayerItemList[ButtonIndex].ItemName;
            Itemtext.text = Inventory.PlayerItemList[ButtonIndex].ItemText;
            Values.text = Inventory.PlayerItemValueList[ButtonIndex].ToString();
            gameObject.SetActive(false);
            ItemPanel.SetActive(true);
        
    }
}
