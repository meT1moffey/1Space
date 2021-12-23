using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public bool music = true;
    public Sprite on, off;
    public GameObject cam;

    void Start()
    {
        // Nothing
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            music = !music;
            GetComponent<SpriteRenderer>().sprite = music ? on : off;
            cam.GetComponent<AudioListener>().enabled = music;
        }
    }

    void Update()
    {
        // Nothing
    }
}
