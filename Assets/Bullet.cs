using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemy;
    public int velocity = 40;
    public float lifetime = 1.0f;
    public Pause pause;

    AudioSource source;
    float prevup = 0.0f, inter = 0.004f;
    Vector3 move;

    void Start()
    {
        prevup = Time.time;
        source = GetComponent<AudioSource>();
        if (gameObject.tag == "Bullet")
        {
            source.Play(0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemy)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if(! pause.isfrozen)
        {
            move = new Vector3(0, velocity * inter, 0);
            transform.position += move;

            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }

            if (gameObject.tag == "Bullet")
            {
                lifetime -= inter;
            }
        }
        if(pause.gameover && (gameObject.tag == "Bullet" || gameObject.tag == "EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
