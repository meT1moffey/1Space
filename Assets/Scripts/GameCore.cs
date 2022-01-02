using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public Background back;
    public Player player;
    public Boss boss;
    public Enemy enemy, big, trash, healenemy;
    public Purifier purifier;
    public float spawnrate = 0.6f, smalrate = 0.99f, afterboss = 1;
    public Pause pause;
    public int maxenemy = 20, enemyneed = 50, enemyleft;

    int globaldiff, nowenemy = 0, curneed;
    bool bossfight, bossapear;
    float prevup = 0, inter = 0.004f, spawnleft = 0, bossleft = 0, localdiff = 0;
    Background[] parts;
    Boss nowboss;
    GameObject curplayer;

    public void Start()
    {
        localdiff = 1.0f;
        globaldiff = 1;
        spawnleft = 0.0f;
        bossfight = false;
        bossapear = false;
        curneed = enemyneed;
        enemyleft = curneed;

        Destroy(curplayer);
        curplayer = GameObject.Instantiate(player.gameObject);
        curplayer.transform.position = new Vector3(0, 0, 0);
        curplayer.tag = "Player";

        if (parts == null)
        {
            parts = new Background[]{ GameObject.Instantiate(back), GameObject.Instantiate(back)};
            parts[0].transform.position = new Vector3(0, 0, 5);
            parts[0].backid = 0;
            parts[0].tag = "Background";

            parts[1].transform.position = new Vector3(0, 32, 5);
            parts[1].backid = 0;
            parts[1].tag = "Background";
        }
    }

    void Update()
    {
        inter = Time.time - prevup;
        prevup = Time.time;

        if (! pause.isfrozen && ! pause.gameover)
        {
            if (bossfight)
            {
                if (nowboss == null && bossapear)
                {
                    if(bossleft <= 0)
                    {
                        Debug.Log("boss defeated");

                        bossleft = afterboss;

                        int newback;
                        do
                        {
                            newback = Random.Range(0, 5);
                        }
                        while (newback == parts[0].backid);

                        parts[0].backid = newback;
                        parts[1].backid = newback;
                    }
                    bossleft -= inter;
                    if(bossleft <= 0)
                    {
                        bossfight = false;
                        bossapear = false;
                    }
                }
                else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    nowboss = GameObject.Instantiate(boss);
                    nowboss.transform.position = new Vector3(0, 20, 0);
                    nowboss.tag = "Enemy";
                    nowboss.lvl = Mathf.Sqrt((float)globaldiff);
                    nowboss.player = curplayer;

                    Debug.Log("boss apeared");
                    bossapear = true;

                    globaldiff++;
                    enemyleft = curneed;
                }
            }
            else
            {
                nowenemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

                spawnleft -= inter;
                if (spawnleft <= 0 && nowenemy < maxenemy)
                {
                    float seed = Random.Range(0.0f, 1.0f);
                    Enemy crew;
                    if(Mathf.Round(seed * 1000) % 100 < 5)
                    {
                        crew = GameObject.Instantiate(trash);
                    }
                    else if (Mathf.Round(seed * 1000) % 100 == 64)
                    {
                        crew = GameObject.Instantiate(healenemy);
                    }
                    else if (Mathf.Round(seed * 1000) % 100 > 97)
                    {
                        Purifier puri = GameObject.Instantiate(purifier);
                        puri.damage = (int)(puri.damage * Mathf.Sqrt(globaldiff));
                        crew = puri;
                    }
                    else if (seed > smalrate / Mathf.Pow(1.2f, localdiff))
                    {
                        crew = GameObject.Instantiate(big);
                    }
                    else
                    {
                        crew = GameObject.Instantiate(enemy);
                    }

                    float xspawn = Random.Range(-7.0f, 7.0f), yspawn = 22.0f;
                    crew.transform.position = new Vector3(xspawn, yspawn, 0);
                    crew.tag = "Enemy";
                    crew.maxhp *= globaldiff;

                    spawnleft += spawnrate / localdiff;
                }

                localdiff = 2.0f - (float)enemyleft/curneed;
                if (localdiff >= 2)
                {
                    Debug.Log("Boss fight");
                    bossfight = true;
                }
            }
        }
    }
}
