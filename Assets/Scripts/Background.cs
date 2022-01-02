using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public int velocity = 10, backid = 0;
    public Pause pause;
    public Sprite[] backs;

    float prevup = 0.0f, inter = 0.004f;
    Vector3 move;

    void Start()
    {
        //Nothing
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if(! pause.isfrozen && ! pause.gameover && gameObject.tag == "Background")
        {
            move = new Vector3(0, -velocity * inter, 0);
            transform.position += move;

            if (transform.position.y <= -32)
            {
                transform.position = new Vector3(0, 32, 5);
                GetComponent<SpriteRenderer>().sprite = backs[backid];
            }
        }
    }
}
