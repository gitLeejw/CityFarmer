using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => StartLogin());
    }

    private void StartLogin()
    {
        // TODO : 계정 로그인 기능 연동 시 추가
        Debug.Log("This is Login Button");
    }
}

