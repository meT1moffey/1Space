using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purifier : Enemy
{
    public float delay = 2;
    public Bullet bullet;
    public int damage = 1;

    float delleft = 0;

    void Update()
    {
        base.Update();
        if(!pause.isfrozen && !pause.gameover)
        {
            delleft -= inter;
            if (delleft <= 0)
            {
                Bullet pew = GameObject.Instantiate(bullet);
                pew.transform.position = transform.position + new Vector3(0, 0, 1);
                pew.tag = "EnemyBullet";
                pew.damage = damage;

                delleft += delay;
            }
        }
    }
}
