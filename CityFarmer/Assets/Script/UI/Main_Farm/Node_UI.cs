using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class Node_UI : MonoBehaviour
{
    public NodeManager Node;
    public List<List<int>> NodeData;
    private void OnEnable()
    {
        OnClickLand(0);
    }
    public void OnClickLand(int LandSeq)
    {
        Node.nodeClick(LandSeq);
        NodeData = Node.ClickNodes.Lands;
        Debug.Log(NodeData[2][1]);
    }
}
