
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Land_UI : MonoBehaviour
{

    public LandManager Land;
    public GameObject timer;
    public Transform CanvesTr;
    private List<GameObject> _timers = new List<GameObject>();
    private List<float> _deltaTime = new List<float>();

    private void Start()
    {
        CreateTimer();
    }
    private void Update()
    {
        for (int nodeIndex = 0; nodeIndex < _deltaTime.Count; nodeIndex++)
        {
            ProgressTime(nodeIndex);
        }
    }
    public GameObject CreateTimerText(Vector3 timerpos, string time)
    {
        GameObject timerText = Instantiate(timer, timerpos, Quaternion.identity, CanvesTr);
        timerText.GetComponent<TextMeshProUGUI>().text = time;
        return timerText;
    }
    private void CreateTimer()
    {
        for (int timerIndex = 0; timerIndex < Land.Nodes.Count; timerIndex++)
        {
            _timers.Add(CreateTimerText(Land.Nodes[timerIndex].GetPosition(), Land.ConvertString(Land.Nodes[timerIndex].GetTimer())));
            float time = Land.Nodes[timerIndex].GetTimer();
            _deltaTime.Add(time);
        }
    }
    private void ProgressTime(int nodeIndex)
    {
        if (_deltaTime[nodeIndex] > 0)
        {
            _deltaTime[nodeIndex] -= Time.deltaTime;
            Land.Nodes[nodeIndex].SetTimer((int)_deltaTime[nodeIndex]);
            _timers[nodeIndex].GetComponent<TextMeshProUGUI>().text = Land.ConvertString((int)_deltaTime[nodeIndex]);
        }
    }


}
