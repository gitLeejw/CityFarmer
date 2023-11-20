using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    private Button _button;
    public GameObject LoginPanel;
    private void Awake()
    {
       
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => LoginPanel.SetActive(true));
    }
}

