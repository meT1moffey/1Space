using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindZone : MonoBehaviour
{
    public bool mouseon = false;

    void Start()
    {
        // Nothing
    }

    void OnMouseEnter()
    {
        mouseon = true;
    }

    void OnMouseExit()
    {
        mouseon = false;
    }

    void Update()
    {
        // Nothing
    }
}
