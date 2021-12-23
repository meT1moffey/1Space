using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public float lifetime = 5.0f;
    public Pause pause;
    public int velocity = 10, value = 1;
    public string type = "scorecoin";

    float prevup = 0.0f, inter = 0.004f, lefttime = 0.0f;
    Vector3 move;

    void Start()
    {
        lefttime = lifetime;
        transform.localScale = new Vector3(Mathf.Sqrt(Mathf.Sqrt(value)) * transform.localScale.x,
                                           Mathf.Sqrt(Mathf.Sqrt(value)) * transform.localScale.y,
                                           transform.localScale.z);

        prevup = Time.time;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if(! pause.isfrozen && ! pause.gameover)
        {
            if (lefttime <= 0 && gameObject.name == "Good(Clone)")
            {
                Destroy(gameObject);
            }
            lefttime -= inter;

            move = new Vector3(0, -velocity * inter, 0);
            transform.position += move;
        }
    }
}
