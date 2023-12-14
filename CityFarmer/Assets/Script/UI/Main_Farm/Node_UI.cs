using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Node_UI : MonoBehaviour
{
    public LandManager LandManager;
    private int _nodeLength;
    private List<Transform> _transforms;
    private float[] _deltaTime = new float[9];
    private Button[] _buttons;
    private Button _closeButton;

    private void Awake()
    {
        InitButton();
    }

    private void OnEnable()
    {
        _nodeLength = LandManager.NodeList.Count;
        _transforms = new List<Transform>();

        for (int childIndex = 0; childIndex < transform.childCount - 1; childIndex++)
        {
            _transforms.Add(transform.GetChild(childIndex));
        }

        ShowButton(LandManager.LandSeq);
    }

    public void ShowButton(int LandSeq)
    {

        for (int nodeIndex = 0; nodeIndex < _nodeLength; nodeIndex++)
        {
            Debug.Log(_nodeLength + "노드랭스");
            Debug.Log(nodeIndex + "노드인덱스");
            Node node = LandManager.NodeList[LandSeq * 9 + nodeIndex];
            int foodSeq = node.GetFoodSeq();
            _deltaTime[nodeIndex] = node.GetTimer();
            Tile tile = node.GetStateNodeTile();
            _transforms[nodeIndex].GetComponent<Image>().sprite = tile.sprite;
        }
    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (_deltaTime[i] > 0)
            {
                _deltaTime[i] -= Time.deltaTime;
            }

            _transforms[i].GetComponentInChildren<TextMeshProUGUI>().text = LandManager.ConvertString((int)_deltaTime[i]);
        }

    }

    //TODO : 버튼 이닛 진우 작업

    private void InitButton()
    {
        _buttons = new Button[transform.childCount - 1];
        
        for (int buttonIndex = 0; buttonIndex < _buttons.Length; ++ buttonIndex)
        {
            _buttons[buttonIndex] = transform.GetChild(buttonIndex).GetComponent<Button>();
            Debug.Log(buttonIndex);
            //_buttons[buttonIndex].onClick.AddListener(() => ASD(buttonIndex));
        }

        _closeButton = transform.GetChild(transform.childCount - 1).GetComponent<Button>();
        _closeButton.onClick.AddListener(() => CloseButton());
    }

    private void CloseButton()
    {
        LandManager.OnNodePopUp = true;
        gameObject.SetActive(false);
    }

    private void ASD(int index)
    {
        Debug.Log(_buttons[index].gameObject.name);
    }
}
