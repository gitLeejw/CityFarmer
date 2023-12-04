using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopUp : MonoBehaviour
{
    enum ProfileInfo
    {
        None,
        ID,
        Name,
        Level,
        Max
    }

    enum ButtonType
    {
        Image,
        Current,
        Max
    }

    private Image _profileImage;
    private int _currentCollectionFoods;
    private int _maxCollectionFoods;

    private TextMeshProUGUI[] _profileInfos;
    private TextMeshProUGUI[] _buttonTexts;

    private Button[] _buttons;
    private Encyclopedia _encyclopedia;
    private UserInfo _userInfo;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitTexts();
        InitButtons();
        // TODO : 테스트 후 동작
        //InitCount();
        //InitTextValue();
    }

    private void InitTexts()
    {
        int minSize = (int)ProfileInfo.ID;
        int maxSize = (int)ProfileInfo.Max;

        _profileInfos = new TextMeshProUGUI[maxSize];

        for (int index = minSize; index < maxSize; ++index)
        {
            //TODO : 현재 버튼 , 텍스트로 바꿀때 변경
            _profileInfos[index] = transform.GetChild(index).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }

    private void InitButtons()
    {
        int arraySize = 3;
        int minChildCount = 4;
        int maxChildCount = 7;

        _buttons = new Button[arraySize];
        _buttonTexts = new TextMeshProUGUI[arraySize];

        int buttonIndex = 0;

        for (int index = minChildCount; index < maxChildCount; ++index, ++buttonIndex)
        {
            _buttons[buttonIndex] = transform.GetChild(index).GetComponent<Button>();

            _buttonTexts[buttonIndex] = transform.GetChild(index).
                GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }

    private void InitCount()
    {
        // 도감 매니저로 연동해야 될 수도 있음.
        _encyclopedia = InfoManager.Instance.gameObject.GetComponent<Encyclopedia>();
        _currentCollectionFoods = _encyclopedia.FoodSeqs.Count;
        _maxCollectionFoods = InfoManager.Instance.Foods.Count;
    }

    private void InitTextValue()
    {
        _userInfo = InfoManager.Instance.gameObject.GetComponent<UserInfo>();

        _profileInfos[(int)ProfileInfo.ID].text = _userInfo.UserId;
        _profileInfos[(int)ProfileInfo.Name].text = _userInfo.UserName;
        _profileInfos[(int)ProfileInfo.Level].text = _userInfo.UserLevel.ToString();
        _buttonTexts[(int)ButtonType.Current].text = _currentCollectionFoods.ToString();
        _buttonTexts[(int)ButtonType.Max].text = _maxCollectionFoods.ToString();
    }

}
