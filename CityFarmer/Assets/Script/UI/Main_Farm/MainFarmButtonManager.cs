using UnityEngine;
using UnityEngine.UI;

public class MainFarmButtonManager : MonoBehaviour
{
    private Button[] _buttons;

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
            string PopUpName = $"UI/{_buttons[currentButtonSize].name}PopUp";

            InitButtonEvent(PopUpName);
        }
    }

    private void InitButtonEvent(string path)
    {
        GameObject popUp = Resources.Load<GameObject>(path);
        GameObject buttonPopUp = Instantiate(popUp, transform.parent);
        buttonPopUp.name = popUp.name;
        buttonPopUp.SetActive(false);
    }
}
