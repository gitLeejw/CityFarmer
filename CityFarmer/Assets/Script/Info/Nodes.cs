using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Nodes
{
    public object _id;
    public int LandSeq;
    public int UserSeq;
    //리스트는 {foodseq, currenttime, state}로 구성되어있음
    //List<int> = { 1,220,2} 푸드 1, 220초 남음, 작물을 기르는 중 
    public List<List<int>> Lands;


}
