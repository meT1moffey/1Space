using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectPlayer : MonoBehaviour
{
    public GameObject select, cam;
    public GameCore gcore;
    public PlayerData playerdata;
    public int cost;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        if(Array.IndexOf(playerdata.data.unlocked, select.name) != -1) {
            gcore.player = select;
            cam.transform.position = new Vector3(0, -50, -10);
        }
        else if(playerdata.data.score >= cost)
        {
            playerdata.data.score -= cost;
            playerdata.data.unlocked = new List<string> (playerdata.data.unlocked) {select.name}.ToArray();
            playerdata.Save();
        }
        else
        {
            Debug.Log("You need do not have enough coins to buy it");
        }
    }

    void Update()
    {
        // Nothing
    }
}
