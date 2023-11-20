using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Money_UI : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI RubyText;
    private Money _money;
    private void Start()
    {
        _money = InfoManager.Instance.Money;
        UpdateGold();
    }
    public void UpdateGold()
    {
        GoldText.text = _money.moneyGold.ToString();
        RubyText.text = _money.moneyRuby.ToString();
    }
}
