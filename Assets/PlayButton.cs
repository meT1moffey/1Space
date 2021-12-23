using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Pause pause;
    public GameObject cam;
    public GameCore gcore;

    AudioSource music;

    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play(0);
    }

    void OnMouseOver()
    {
        pause.gameover = false;
        cam.transform.position = new Vector3(0, 0, -10);
        gcore.Start();
    }

    void Update()
    {
        if(! pause.gameover && ! pause.isfrozen)
        {
            music.Pause();
        }
        else
        {
            music.UnPause();
        }
    }
}
