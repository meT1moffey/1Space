using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isfrozen = false, gameover = false;

    AudioSource music;

    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play(0);
    }

    void OnMouseDown()
    {
        if(! gameover)
        {
            isfrozen = true;
        }
    }

    void Update()
    {
        if (!gameover && !isfrozen)
        {
            music.UnPause();
        }
        else
        {
            music.Pause();
        }
    }
}
