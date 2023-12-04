using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory_UI : MonoBehaviour
{
    //TODO : 아이템 세부확인서 연결
    //public GameObject ItemPanel;
    //public Image Itemimage;
    //public TMP_Text Values;
    //public TMP_Text Itemname;
    //public TMP_Text Itemtext;

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
        ShowButtonFood(panel);


        panel = transform.GetChild(1);
        GameManager.InventoryManager.PlayerItemList = GameManager.InventoryManager.PlayerItemList.OrderBy(obj => obj.itemType).ToList();
        ShowButtonItem(panel, GetCostumeIndex(GameManager.InventoryManager.PlayerItemList), 0);
        panel = transform.GetChild(2);

        if (GetCostumeIndex(GameManager.InventoryManager.PlayerItemList) > 0)
        {
            ShowButtonItem(panel, GameManager.InventoryManager.PlayerItemList.Count, GetCostumeIndex(GameManager.InventoryManager.PlayerItemList));
        }
    }
    public void ShowButtonFood(Transform panel)
    {
        for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
        {
            if (GameManager.InventoryManager.PlayerFoodList.Count > childIndex)
            {
                if (panel.GetChild(childIndex).name.Contains("ItemButton"))
                {
                    Transform button = panel.GetChild(childIndex);
                    button.GetComponent<Image>().sprite = GameManager.InventoryManager.PlayerFoodList[childIndex].FoodSprite;
                    button.GetComponentInChildren<TMP_Text>().text = GameManager.InventoryManager.PlayerFoodList[childIndex].FoodValue.ToString();
                }
            }
            else
            {
                break;
            }
        }
    }
    void ShowButtonItem(Transform panel, int Count, int StartCount)
    {
        int sellCount = 0;
        for (int childIndex = StartCount; childIndex < Count; childIndex++)
        {
            if (GameManager.InventoryManager.PlayerItemList.Count > childIndex)
            {
                if (panel.GetChild(childIndex).name.Contains("ItemButton"))
                {
                    Transform button = panel.GetChild(sellCount);
                    button.GetComponent<Image>().sprite = GameManager.InventoryManager.PlayerItemList[childIndex].ItemSprite;
                    button.GetComponentInChildren<TMP_Text>().text = GameManager.InventoryManager.PlayerItemList[childIndex].ItemValue.ToString();
                    sellCount++;
                }
            }
            else
            {
                break;
            }
        }
    }
    //public void InfoFoodClick()
    //{
    //    int ButtonIndex = 0;
    //    GameObject _gameObject = EventSystem.current.currentSelectedGameObject;
    //    Transform panel = transform.GetChild(0);

    //    for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
    //    {

    //       if (panel.GetChild(childIndex).name.Equals(_gameObject.name))
    //       {
    //           ButtonIndex = childIndex;
    //            Debug.Log(ButtonIndex);
    //       }

    //    }
    //    Itemimage.sprite = GameManager.InventoryManager.PlayerFoodList[ButtonIndex].FoodSprite;
    //    Itemname.text = GameManager.InventoryManager.PlayerFoodList[ButtonIndex].FoodName;
    //    Itemtext.text = GameManager.InventoryManager.PlayerFoodList[ButtonIndex].FoodText;
    //    Values.text = GameManager.InventoryManager.PlayerFoodList[ButtonIndex].FoodValue.ToString();

    //    gameObject.SetActive(false);
    //    //ItemPanel.SetActive(true);
    //}

    private int GetCostumeIndex(List<Item> items)
    {
        int index;
        for (index = 0; index < items.Count; index++)
        {
            if (items[index].itemType == Item.ItemType.Costume)
            {
                return index;
            }
        }
        return -1;


    }
    //public void InfoItemClick()
    //{
    //    int ButtonIndex = 0;
    //    GameObject _gameObject = EventSystem.current.currentSelectedGameObject;
    //    Transform panel = transform.GetChild(0);

    //    for (int childIndex = 0; childIndex < panel.childCount; childIndex++)
    //    {

    //        if (panel.GetChild(childIndex).name.Equals(_gameObject.name))
    //        {
    //            ButtonIndex = childIndex;
    //            Debug.Log(ButtonIndex);
    //        }

    //    }            
    //    Itemimage.sprite = GameManager.InventoryManager.PlayerItemList[ButtonIndex].ItemSprite;
    //    Itemname.text = GameManager.InventoryManager.PlayerItemList[ButtonIndex].ItemName;
    //    Itemtext.text = GameManager.InventoryManager.PlayerItemList[ButtonIndex].ItemText;
    //    Values.text = GameManager.InventoryManager.PlayerItemList[ButtonIndex].ItemValue.ToString();

    //    gameObject.SetActive(false);
    //    //ItemPanel.SetActive(true);
    //}
}
