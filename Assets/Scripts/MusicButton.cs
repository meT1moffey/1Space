using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public Sprite on, off;
    public GameObject cam;

    bool music = true;
    int clickcount = 0;
    float firstclick = 0, lastclick = 0;

    void Start()
    {
        // Nothing
    }

    void OnMouseDown()
    {
        music = !music;
        GetComponent<SpriteRenderer>().sprite = music ? on : off;
        AudioListener.volume = music ? 1 : 0;
        if(Time.time - firstclick < 6)
        {
            clickcount++;
            if(clickcount == 6)
            {
                lastclick = Time.time;
            }
        }
        else
        {
            clickcount = 1;
            firstclick = Time.time;
        }
    }

    void Update()
    {
        if(clickcount == 6 && Time.time - lastclick > 1)
        {
            cam.transform.position = new Vector3(-100, -50, -10);
        }
    }
}
