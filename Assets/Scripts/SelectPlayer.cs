using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectPlayer : MonoBehaviour
{
    public Player select;
    public GameCore gcore;
    public DataCore dcore;
    public ShowData showcost;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = select.GetComponent<SpriteRenderer>().sprite;

        showcost.Init();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Array.IndexOf(dcore.data.unlocked, select.name) != -1)
            {
                gcore.player = select;
                dcore.data.pick = select.name;
            }
            else if (dcore.data.score >= select.cost)
            {
                gcore.player = select;
                dcore.data.pick = select.name;

                dcore.data.score -= select.cost;
                dcore.data.unlocked = new List<string>(dcore.data.unlocked) { select.name }.ToArray();
                dcore.Save();

                showcost.Clear();
            }
            else
            {
                Debug.Log("You need do not have enough coins to buy it");
            }
        }
    }

    void Update()
    {
        if (Array.IndexOf(dcore.data.unlocked, select.name) == -1)
        {
            showcost.Show(select.cost);
        }
    }
}
