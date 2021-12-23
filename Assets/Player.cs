using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int vel = 25, maxhp = 5, distvel = 10;
    public float delay = 0.75f, restime = 2.0f;
    public GameObject bullet, deathscreen, scorenum;
    public Pause pause;
    public Sprite[] nums;
    public PlayerData playerdata;

    double yvel = 0, xvel = 0;
    double[] angle = {0, 1};
    bool hold;
    float prevup = 0.0f, inter = 0.004f, delleft = 0.0f, dist, resleft = 0.0f;
    int score, hp, currmaxhp;
    GameObject[] shownum, showhp, showdist;
    Vector3 move;

    double Distance(Vector2 p1, Vector2 p2)
    {
        float xdist = p2.x - p1.x, ydist = p2.y - p1.y;
        return Mathf.Sqrt(xdist * xdist + ydist * ydist);
    }

    double[] Angle(Vector2 p1, Vector2 p2)
    {
        float xdist = p2.x - p1.x, ydist = p2.y - p1.y;
        double dist = Distance(p1, p2);

        double[] res = {xdist / dist, ydist / dist};
        return res;
    }

    public void Start()
    {
        prevup = Time.time;

        hp = maxhp;
        currmaxhp = maxhp;
        GameObject[] prevnums = GameObject.FindGameObjectsWithTag("Number");
        for(int i = 0; i < prevnums.Length; i++)
        {
            Destroy(prevnums[i]);
        }
        if (shownum == null)
        {
            showhp = new GameObject[1];
            shownum = new GameObject[1];
            showdist = new GameObject[1];
        }
        score = playerdata.data.score;
        dist = 0;
        hold = false;

        deathscreen.GetComponent<AudioSource>().Pause();
    }

    void Funeral()
    {
        deathscreen.transform.position = new Vector3(0, 0, -3);

        deathscreen.GetComponent<AudioSource>().Play(0);

        pause.gameover = true;

        playerdata.data.score = score;
        playerdata.Save();

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {
            if(resleft <= 0)
            {
                hp--;

                if (hp > 0)
                {
                    GetComponent<AudioSource>().Play(0);
                    resleft = restime;
                }
            }
        }
        else if(collision.tag == "Bonus")
        {
            Bonus bonus = collision.gameObject.GetComponent<Bonus>();

            if(bonus.type == "scorecoin")
            {
                score += bonus.value;
            }
            else if(bonus.type == "hpboost")
            {
                currmaxhp += bonus.value;
                hp = currmaxhp;
            }
            else if(bonus.type == "heal")
            {
                hp += bonus.value;
                if(hp > currmaxhp)
                {
                    hp = currmaxhp;
                }
            }
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;
        if(!pause.isfrozen && !pause.gameover && gameObject.tag == "Player")
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                hold = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                hold = false;
            }

            if (hold && Distance(transform.position, mouse) > 1)
            {
                angle = Angle(transform.position, mouse);

                xvel = vel * angle[0];
                yvel = vel * angle[1];
            }
            else
            {
                xvel = 0;
                yvel = 0;
            }

            delleft -= inter;
            if (delleft <= 0)
            {
                GameObject pew = GameObject.Instantiate(bullet);
                pew.transform.position = transform.position;
                pew.tag = "Bullet";

                delleft += delay;
            }

            for (int i = 0; i < shownum.Length; i++)
            {
                Destroy(shownum[i]);
            }
            shownum = new GameObject[score.ToString().Length];
            for (int i = 0; i < score.ToString().Length; i++)
            {
                shownum[i] = GameObject.Instantiate(scorenum);
                shownum[i].transform.position = new Vector3(-2 * i + 2 * score.ToString().Length - 8, 14, -5);
                shownum[i].GetComponent<SpriteRenderer>().sprite = nums[(int)(score / Mathf.Pow(10, i)) % 10];
                shownum[i].tag = "Number";
            }

            for (int i = 0; i < showhp.Length; i++)
            {
                Destroy(showhp[i]);
            }
            showhp = new GameObject[hp.ToString().Length];
            for (int i = 0; i < hp.ToString().Length; i++)
            {
                showhp[i] = GameObject.Instantiate(scorenum);
                showhp[i].transform.position = new Vector3(-2 * i + 2 * hp.ToString().Length - 8, 17, -5);
                showhp[i].GetComponent<SpriteRenderer>().sprite = nums[(int)(hp / Mathf.Pow(10, i)) % 10];
                showhp[i].tag = "Number";
            }

            for (int i = 0; i < showdist.Length; i++)
            {
                Destroy(showdist[i]);
            }
            int intdist = (int)dist;
            showdist = new GameObject[intdist.ToString().Length];
            for (int i = 0; i < intdist.ToString().Length; i++)
            {
                showdist[i] = GameObject.Instantiate(scorenum);
                showdist[i].transform.position = new Vector3(-2 * i + 2 * intdist.ToString().Length - 8, 11, -5);
                showdist[i].GetComponent<SpriteRenderer>().sprite = nums[(int)(intdist / Mathf.Pow(10, i)) % 10];
                showdist[i].tag = "Number";
            }

            move = new Vector3((float)xvel * inter, (float)yvel * inter, 0);
            dist += distvel * inter;
            transform.position += move;

            if (hp == 0)
            {
                Funeral();
            }

            if (resleft > 0)
            {
                resleft -= inter;
            }
        }
    }
}