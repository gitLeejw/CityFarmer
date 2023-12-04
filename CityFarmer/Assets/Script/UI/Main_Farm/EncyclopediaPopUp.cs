using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPopUp : MonoBehaviour
{

    private TextMeshProUGUI _currentCollection;
    private TextMeshProUGUI _maxCollection;


    private int _currentTextIndex = 1;
    private int _maxTextIndex = 2;
    private int _encyclopediaIndex = 3;
    private GameObject _encyclopediaFoods;
    private List<Food> _foods;
    private void Awake()
    {
        Init();
        InitFoods();
        //InitCollectionCount();
    }

    private void Init()
    {
        _currentCollection = transform.GetChild(_currentTextIndex).GetComponent<TextMeshProUGUI>();
        _maxCollection = transform.GetChild(_maxTextIndex).GetComponent<TextMeshProUGUI>();
        _encyclopediaFoods = transform.GetChild(_encyclopediaIndex).gameObject;

    }

    private void InitCollectionCount()
    {
        _currentCollection.text = MainUIManager.EncyclopediaManager.CurrentCollectionFoods.ToString();
        _maxCollection.text = MainUIManager.EncyclopediaManager.MaxCollectionFoods.ToString();
    }

    private void InitFoods()
    {
        _foods = InfoManager.Instance.Foods;

        //TODO : Çªµå °¹¼ö¿¡ µû¶ó¼­ 

        for(int foodCount =0; foodCount < _foods.Count; ++ foodCount)
        {
            Image foodImage = _encyclopediaFoods.transform.GetChild(foodCount).GetComponent<Image>();
            foodImage.sprite = _foods[foodCount].FoodSprite;
        }
    }
}
