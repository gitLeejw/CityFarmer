using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[Serializable]
public class Node
{
    List<Nodes> node = new List<Nodes>();
    public enum NodeState
    {

        None,
        Cultivating,
        Harvesting

    }
    private Vector3Int _position;
    private int _foodSeq;
    private int _timer;
   
    public NodeState State { get; set; }
    private Tile _nodeTile;
    public Node(Vector3Int position,int foodSeq,int timer)
    {
        _position = position;
        _foodSeq = foodSeq;
        _timer = timer;
    }
    public Vector3Int GetPosition()
    {
        return _position;
    }
    public int GetFoodSeq()
    {
        return _foodSeq;
    }
    public int GetTimer()
    {
        return _timer;
    }
    public void SetTimer(int timer)
    { 
        _timer = timer;
    }
    public Tile GetStateNodeTile()
    {
        return Resources.Load<Tile>("Tile/Node/"+State.ToString());
    }
    public void SetNodeTile()
    {
        _nodeTile = GetStateNodeTile();
    }

    
}
