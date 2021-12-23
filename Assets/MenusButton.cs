using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusButton : MonoBehaviour
{
    public GameObject cam;
    public Vector3 coords;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        cam.transform.position = coords;
    }

    void Update()
    {
        // Nothing
    }
}
