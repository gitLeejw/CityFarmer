using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainFarmButtonManager : MonoBehaviour
{
    private Button[] _buttons;
    private GameObject _beforeButton;

    enum ButtonType
    {
        Profile,
        Goods,
        Encyclopedia,
        Inventory,
        Setting,
        Max
    }

    private void Awake()
    {
        InitButton();
    }

    private void InitButton()
    {
        int maxButtonSize = (int)ButtonType.Max;

        _buttons = new Button[maxButtonSize];

        for (int currentButtonSize = 0; currentButtonSize < maxButtonSize; ++currentButtonSize)
        {
            _buttons[currentButtonSize] = transform.GetChild(currentButtonSize).GetComponent<Button>();
            string PopUpPath = $"UI/Main_Farm/{_buttons[currentButtonSize].name}PopUp";
            InitButtonPopUp(PopUpPath, _buttons[currentButtonSize]);
        }
    }

    private void InitButtonPopUp(string path, Button button)
    {
        GameObject popUp = Resources.Load<GameObject>(path);

        if (popUp != null)
        {
            GameObject buttonPopUp = Instantiate(popUp, transform.parent);
            buttonPopUp.name = popUp.name;
            buttonPopUp.SetActive(false);

            InitButtonEvent(button, buttonPopUp);
        }
    }

    private void InitButtonEvent(Button button, GameObject gameObject)
    {
        button.onClick.AddListener(() => CloseButton(gameObject));

        void CloseButton(GameObject clickButton)
        {
            if (_beforeButton == clickButton)
            {
                if (_beforeButton.activeSelf == true)
                {
                    _beforeButton.SetActive(false);
                }
                else
                {
                    _beforeButton.SetActive(true);
                }
            }
       
            else
            {
                if(_beforeButton != null)
                {
                    _beforeButton.SetActive(false); 
                }

                _beforeButton = clickButton;

                _beforeButton.SetActive(true);
            }
        }
    }
}