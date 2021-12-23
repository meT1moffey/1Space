using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int velocity = 20, maxhp = 1, bonusval = 1;
    public float lifetime = 2.0f;
    public GameObject explosion;
    public GameCore gcore;
    public Pause pause;
    public Bonus bonus;

    float prevup = 0.0f, inter = 0.004f, lefttime = 0.0f;
    Vector3 move;
    int hp;

    void Start()
    {
        lefttime = lifetime;
        hp = maxhp;
        prevup = Time.time;
    }

    void Funeral()
    {
        Bonus yay = GameObject.Instantiate(bonus);
        yay.transform.position = transform.position;
        yay.tag = "Bonus";
        yay.value = maxhp;

        GameObject boom = GameObject.Instantiate(explosion);
        boom.transform.position = transform.position;
        boom.transform.position += new Vector3(0, 0, 1);
        boom.tag = "Background";
        boom.GetComponent<AudioSource>().Play(0);

        gcore.enemyleft -= bonusval;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            hp--;
        }
        if(hp == 0)
        {
            Funeral();
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if(! pause.isfrozen && ! pause.gameover)
        {
            move = new Vector3(0, -velocity * inter, 0);
            transform.position += move;

            if (lefttime <= 0 && gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
            lefttime -= inter;

            if(gameObject.name == "Trash(Clone)")
            {
                transform.Rotate(new Vector3(0, 0, 10), 10, Space.Self);
            }
        }
    }
}
