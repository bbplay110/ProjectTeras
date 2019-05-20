using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour {
    public GameObject SentryBody;
	protected GameObject Player;
	public float viewDist;

    protected float Dis;
    public float ShootRate;
    protected float Timer;
    public GameObject Bullet;
	public GameObject BulletPosition;
    public Transform PlayerBody;
    protected Ray eyeContect;
    protected bool See=false;
    protected bool Dead=false;
	// Use this for initialization
	void Start () {
        PlayerBody = GameObject.Find("Player").transform;
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
       
        FindPlayer();
        ChackingSeePlayer();
	}
    protected virtual void FindPlayer()
    {
        BulletPosition.transform.LookAt(Player.transform.position);
        Dis = Vector3.Distance(Player.transform.position, transform.position);
        if (Dis < viewDist && !Dead&&See)
        {
            SentryBody.transform.LookAt(Player.transform.position);
            Timer -= 1 * Time.deltaTime;
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
    protected virtual void ChackingSeePlayer() {
        RaycastHit hit;
        eyeContect.origin = BulletPosition.transform.position;
        eyeContect.direction = Player.transform.position-BulletPosition.transform.position;
        //Debug.DrawLine(eyeContect.origin, eyeContect.GetPoint(viewDist),Color.red,viewDist,true);
        if(Physics.Raycast(eyeContect,out hit,viewDist)){
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
    public void isDeath()
    {
        Dead = true;
    }
	public virtual void Shooting(){
        Timer = ShootRate;
        Instantiate(Bullet,BulletPosition.transform.position,BulletPosition.transform.rotation);
	}
}
