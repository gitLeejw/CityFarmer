using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTexture
{
    [SerializeField]
    List<GameObject> mapTexture = new List<GameObject>();

    public GameObject GetList(int index)
    {
        return mapTexture[index];
    }
    public int GetCountOfIndex()
    {
        return mapTexture.Count;
    }
}


public class Land : MonoBehaviour
{
    
    public enum LandState
    {
        Harvesting,
        Cultivating,
        None

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
