using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int vel = 25, maxhp = 5, distvel = 10, cost = 0, damage = 1, maxmaxhp = 20;
    public float delay = 0.75f, restime = 2;
    public GameObject deathscreen, shield;
    public Bullet bullet;
    public Pause pause;
    public DataCore dcore;
    public ShowData showhp, shownum, showdist;
    public Super super;
    public bool immortal = false;
    public BlindZone[] blindzones;

    double yvel = 0, xvel = 0;
    double[] angle = {0, 1};
    bool hold = false;
    float prevup = 0, inter = 0.004f, delleft = 0, dist, resleft = 0;
    int score, hp, currmaxhp;
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
        showhp.Init();
        shownum.Init();
        showdist.Init();
        
        score = dcore.data.score;
        dist = 0;

        deathscreen.GetComponent<AudioSource>().Pause();

        super.owner = this;
        super.Init();
    }

    void Funeral()
    {
        deathscreen.transform.position = new Vector3(0, 0, -3);

        deathscreen.GetComponent<AudioSource>().Play(0);

        pause.gameover = true;

        dcore.data.score = score;
        dcore.Save();

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(resleft <= 0 && !immortal)
            {
                hp--;

                if (hp > 0)
                {
                    GetComponent<AudioSource>().Play(0);
                    resleft = restime;
                }
            }
        }
        else if(collision.tag == "EnemyBullet")
        {
            if (resleft <= 0 && !immortal)
            {
                hp -= collision.gameObject.GetComponent<Bullet>().damage;

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
                currmaxhp = Mathf.Min(currmaxhp + bonus.value, maxmaxhp);
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

            foreach(BlindZone bz in blindzones)
            {
                if(bz.mouseon)
                {
                    hold = false;
                    break;
                }
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
                Bullet pew = GameObject.Instantiate(bullet);
                pew.transform.position = transform.position + new Vector3(0, 0, 1);
                pew.tag = "Bullet";
                pew.damage = damage;
                
                delleft += delay;
            }

            showhp.Show(hp);
            shownum.Show(score);
            showdist.Show((int)dist);

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

            super.Update(inter);

            if(immortal)
            {
                shield.transform.position = transform.position + new Vector3(0, 0, -1);
            }
            else
            {
                shield.transform.position = new Vector3(20, 0, 1);
            }
        }
    }
}

[System.Serializable]
public class Super
{
    public float reloadtime = 15, value = 1, bonustime = 1;
    public string type = "damageboost";
    public Pause pause;
    public ShowData showreload;
    public Player owner;
    public SuperButton button;

    float reloadleft, bonusleft = 0;
    bool activated = false;

    public void Init()
    {
        showreload.Init();

        button.super = this;

        reloadleft = reloadtime;
    }

    public void Activate()
    {
        if(reloadleft > 0)
        {
            Debug.Log("Super does not charged yet");
        }
        else
        {
            bonusleft = bonustime;
            reloadleft = reloadtime;
            activated = true;

            if (type == "damageboost")
            {
                owner.damage = (int)(owner.damage * value);
                owner.bullet.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            }
            else if (type == "speedboost")
            {
                owner.vel = (int)(owner.vel * value);
                owner.delay /= value;
            }
            else if(type == "shield")
            {
                owner.immortal = true;
            }

            Debug.Log("Super Activated");
        }
    }

    public void Deactivate()
    {
        if (type == "damageboost")
        {
            owner.damage = (int)(owner.damage / value);
            owner.bullet.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else if (type == "speedboost")
        {
            owner.vel = (int)(owner.vel / value);
            owner.delay *= value;
        }
        else if (type == "shield")
        {
            owner.immortal = false;
        }

        Debug.Log("Super Deactivated");
    }

    public void Update(float inter)
    {
        if (bonusleft > 0)
        {
            bonusleft -= inter;
        }
        else
        {
            if(activated)
            {
                activated = false;
                Deactivate();
            }
            reloadleft = Mathf.Max(reloadleft - inter, 0);

            showreload.Show((int)Mathf.Ceil(reloadleft));
        }
    }
}