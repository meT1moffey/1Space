using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 1.0f;
    public Pause pause;

    float prevup = 0.0f, inter = 0.004f, lefttime = 0.0f;

    void Start()
    {
        lefttime = lifetime;

        prevup = Time.time;
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if (! pause.isfrozen && ! pause.gameover)
        {
            if (lefttime <= 0 && gameObject.tag == "Background")
            {
                Destroy(gameObject);
            }
            lefttime -= inter;
        }
    }
}
