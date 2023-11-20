using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField IdInputField;
    public TMP_InputField PassWordInputField;
    public TMP_InputField NameInputField;
    
   
    public void Login()
    {
        if (InfoManager.Instance.Login(IdInputField.text, PassWordInputField.text))
        {

            InfoManager.Instance.LoadFood();
            InfoManager.Instance.LoadItem();
            InfoManager.Instance.LoadMoney();
            gameObject.SetActive(false);
        }
    }
    public void Sign()
    {
        InfoManager.Instance.SignUp(IdInputField.text, PassWordInputField.text,NameInputField.text);
        InfoManager.Instance.InsertMoney();
        Login();
    }

}
