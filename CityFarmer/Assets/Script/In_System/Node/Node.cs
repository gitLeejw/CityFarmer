using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : Tile
{
    public Sprite[] NodeSprites;
    private int userLevel;
    private UserInfo userInfo;
    void Start()
    {
        userInfo = GameObject.Find("GameManager").GetComponent<UserInfo>();
        userLevel = userInfo.UserLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
