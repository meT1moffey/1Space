using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Pause pause;
    public GameCore gcore;
    public GameObject bsod, cam;

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
        pause.gameover = true;

        cam.transform.position = new Vector3(0, -50, -10);
    }

    void Update()
    {
        if (pause.isfrozen)
        {
            transform.position = new Vector3(0, -5, -5);
        }
        else if(pause.gameover)
        {
            transform.position = new Vector3(3, -8, -5);
        }
        else
        {
            transform.position = new Vector3(20, 10, -5);
        }
    }
}