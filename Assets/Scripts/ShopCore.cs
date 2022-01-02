using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCore : MonoBehaviour
{
    public Player[] selects;
    public DataCore dcore;
    public SelectPlayer plbutton;
    public ShowData showscore;
    public GameObject frame;

    SelectPlayer[] buttons;

    void Start()
    {
        buttons = new SelectPlayer[selects.Length];

        for(int i = 0; i < selects.Length; i++)
        {
            buttons[i] = GameObject.Instantiate(plbutton);
            buttons[i].transform.position = new Vector3((i % 2 == 0 ? -5 : 5), -95 - 12*(int)(i / 2), 0);

            buttons[i].select = selects[i];
            buttons[i].showcost.coord = buttons[i].transform.position + new Vector3(3, -5, 0);
        }

        showscore.Init();
    }

    void Update()
    {
        showscore.Show(dcore.data.score);

        foreach(SelectPlayer b in buttons)
        {
            if(b.select.name == dcore.data.pick)
            {
                frame.transform.position = b.transform.position + new Vector3(0, 0, 1);
                break;
            }
        }
    }
}
