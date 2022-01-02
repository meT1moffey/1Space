using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public Pause pause;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pause.isfrozen = false;
        }
    }

    void Update()
    {
        if(pause.isfrozen)
        {
            transform.position = new Vector3(0, 5, -5);
        }
        else
        {
            transform.position = new Vector3(20, 5, -5);
        }
    }
}
