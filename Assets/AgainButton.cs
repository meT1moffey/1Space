using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgainButton : MonoBehaviour
{
    public GameCore gcore;
    public Pause pause;
    public GameObject bsod;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        gcore.Start();

        var cl = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var c in cl)
        {
            Destroy(c);
        }
        cl = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var c in cl)
        {
            Destroy(c);
        }
        cl = GameObject.FindGameObjectsWithTag("Bonus");
        foreach (var c in cl)
        {
            Destroy(c);
        }
        bsod.transform.position = new Vector3(-20, 0, 5);

        pause.isfrozen = false;
        pause.gameover = false;
    }

    void Update()
    {
        if (pause.isfrozen)
        {
            transform.position = new Vector3(0, 0, -5);
        }
        else if (pause.gameover)
        {
            transform.position = new Vector3(-3, -8, -5);
        }
        else
        {
            transform.position = new Vector3(20, 10, -5);
        }
    }
}
