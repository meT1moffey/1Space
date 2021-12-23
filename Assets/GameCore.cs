using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public GameObject back, player;
    public Boss boss;
    public Enemy enemy, big, trash, healenemy;
    public float spawnrate = 0.6f, smalrate = 0.99f, afterboss = 1.0f;
    public Pause pause;
    public int maxenemy = 20, enemyneed = 50, enemyleft;

    int globaldiff, nowenemy = 0, curneed;
    bool bossfight, bossapear;
    float prevup = 0.0f, inter = 0.004f, spawnleft = 0.0f, bossleft = 0.0f, localdiff = 0.0f;
    GameObject[] parts;
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
        curplayer = GameObject.Instantiate(player);
        curplayer.transform.position = new Vector3(0, 0, 0);
        curplayer.tag = "Player";

        if (parts == null)
        {
            parts = new GameObject[]{ GameObject.Instantiate(back), GameObject.Instantiate(back)};
            parts[0].transform.position = new Vector3(0, 0, 5);
            parts[1].transform.position = new Vector3(0, 32, 5);
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
                    Debug.Log("boss defeated");
                    if(bossleft <= 0)
                    {
                        bossleft = afterboss;
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
