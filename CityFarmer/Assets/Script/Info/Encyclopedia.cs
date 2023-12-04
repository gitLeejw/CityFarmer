using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Encyclopedia : MonoBehaviour
{
    // 몽고DB 초기화 시 필요 변수
    public object _id;
    public int UserSeq;
    public List<int> FoodSeqs;
}
