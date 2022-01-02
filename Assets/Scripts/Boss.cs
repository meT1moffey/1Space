using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int velocity = 10, maxhp = 20;
    public float delay = 2.0f, lvl = 1.0f;
    public GameObject player, explosion, bullet;
    public Bonus boost, coin;
    public Pause pause;

    float prevup = 0.0f, inter = 0.004f, delleft = 0.0f;
    int hp;
    Vector3 move;

    void Start()
    {
        maxhp = (int)((float)maxhp*lvl);
        velocity = (int)((float)velocity * lvl);
        hp = maxhp;

        prevup = Time.time;
    }

    void Funeral()
    {
        Bonus yay = GameObject.Instantiate(boost);
        yay.transform.position = transform.position;
        yay.tag = "Bonus";

        yay = GameObject.Instantiate(coin);
        yay.transform.position = transform.position;
        yay.tag = "Bonus";
        yay.value = maxhp;

        GameObject boom = GameObject.Instantiate(explosion);
        boom.transform.position = transform.position;
        boom.transform.position += new Vector3(0, 0, 1);
        boom.tag = "Background";
        boom.GetComponent<AudioSource>().Play(0);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Player")
        {
            hp--;
        }
        if (hp == 0)
        {
            Funeral();
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if (! pause.isfrozen && ! pause.gameover && gameObject.tag == "Enemy")
        {
            if(transform.position.y > 15)
            {
                move = new Vector3(0, -velocity * inter, 0);
                transform.position += move;
            }
            else
            {
                delleft -= inter;
                if (delleft <= 0)
                {
                    GameObject pew = GameObject.Instantiate(bullet);
                    pew.transform.position = transform.position;
                    pew.tag = "EnemyBullet";

                    delleft += delay;
                }

                if(player.transform.position.x - transform.position.x > 1)
                {
                    move = new Vector3(velocity * inter, 0, 0);
                }
                else if(transform.position.x - player.transform.position.x > 1)
                {
                    move = new Vector3(-velocity * inter, 0, 0);
                }
                else
                {
                    move = new Vector3(0, 0, 0);
                }
                transform.position += move;
            }
        }
    }
}
