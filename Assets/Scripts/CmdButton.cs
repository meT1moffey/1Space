using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdButton : MonoBehaviour
{
    public string action;
    public DataCore dcore;
    void Start()
    {
        // Nothing
    }

    void OnMouseDown()
    {
        if(action == "addscore")
        {
            dcore.data.score += 1000000;
            dcore.Save();
        }
        if(action == "reset")
        {
            dcore.Delete();
        }
        if(action == "unlock")
        {
            Player[] players = GameObject.FindObjectOfType<ShopCore>().selects;
            string[] names = new string[players.Length];
            for(int i = 0; i < players.Length; i++)
            {
                names[i] = players[i].name;
            }
            dcore.data.unlocked = names;
            dcore.Save();
        }
    }

    void Update()
    {
        // Nothing
    }
}
