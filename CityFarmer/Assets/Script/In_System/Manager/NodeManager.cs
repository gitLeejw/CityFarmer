using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
    public class NodeManager : MonoBehaviour
{
    
    public Tilemap tilemap;
    Vector3 localPos;
    
   
    
    void Start()
    {
       
        Vector3Int localToCellPos = tilemap.LocalToCell(localPos);
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            
            var tile = tilemap.GetTile<TileBase>(pos);
            
            // 정보 초기화
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
