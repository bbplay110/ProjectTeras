using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVEL1Turret :MonoBehaviour {
    public  GameObject[] SentryBodys;
    public  GameObject[] BulletPositions;
    protected GameObject Player;
    public float viewDist;
    protected float Dis;
    public float ShootRate;
    protected float Timer;
    public GameObject Bullet;
    public Transform PlayerBody;
    protected Ray eyeContect;
    protected bool See = false;
    protected bool Dead = false;
    void Start()
    {
        PlayerBody = GameObject.Find("Player").transform;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        FindPlayer();
        ChackingSeePlayer();
    }
    // Use this for initialization
    protected  void ChackingSeePlayer()
    {
        RaycastHit hit;
        eyeContect.origin = BulletPositions[0].transform.position;
        eyeContect.direction = Player.transform.position - BulletPositions[0].transform.position;
        //Debug.DrawLine(eyeContect.origin, eyeContect.GetPoint(viewDist),Color.red,viewDist,true);
        if (Physics.Raycast(eyeContect, out hit, viewDist))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                See = true;
            }
            else
            {
                See = false;
            }

        }
    }

    protected  void FindPlayer()
    {
        foreach (var item in BulletPositions)
        {
            item.transform.LookAt(Player.transform.position);
        }

        Dis = Vector3.Distance(Player.transform.position, transform.position);
        if (Dis < viewDist && !Dead && See)
        {
            iTween.LookUpdate(SentryBodys[0],iTween.Hash("looktarget",Player.transform.position, "axis","y", "time",0.5f));
            //iTween.LookUpdate(SentryBodys[1], iTween.Hash("looktarget", Player.transform.position, "axis", "z", "time", 0.5f));
            //iTween.LookUpdate(SentryBodys[2], iTween.Hash("looktarget", Player.transform.position, "axis", "z", "time", 0.5f));
            //SentryBodys[0].transform.LookAt(Player.transform.position, SentryBodys[0].transform.up);
            //SentryBodys[1].transform.LookAt(Player.transform.position, SentryBodys[1].transform.right);
            //SentryBodys[2].transform.LookAt(Player.transform.position, -SentryBodys[2].transform.right);
            //iTween.LookTo(SentryBody,,);
            Timer -= 1*Time.deltaTime;
            if (Timer <= 0)
            {
                Shooting();
            }
        }
        else
        {
            Timer = ShootRate;
        }
    }

    public void Shooting()
    {
        Timer = ShootRate;
        foreach(var item in BulletPositions)
        {
            Instantiate(Bullet,item.transform.position, item.transform.rotation);
        }
        
    }
    public void isDeath()
    {
        Dead = true;
    }
}
