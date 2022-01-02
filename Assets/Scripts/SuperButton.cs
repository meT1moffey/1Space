using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperButton : MonoBehaviour
{
    public Super super;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            super.Activate();
        }
    }

    void Update()
    {
        // Nothing
    }
}
