using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour {
    public GameObject SentryBody;
	private GameObject Player;
	public float viewDist;

    private float Dis;
    public float ShootRate;
    private float Timer;
    public GameObject Bullet;
	public GameObject BulletPosition;
    public Transform PlayerBody;
    private Ray eyeContect;
    private bool See=false;
    private bool Dead=false;
	// Use this for initialization
	void Start () {
        PlayerBody = GameObject.Find("Motoko_Kusanag_root").transform;
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        BulletPosition.transform.LookAt(Player.transform.position);
        FindPlayer();
        
        ChackingSeePlayer();
	}
    private void FindPlayer()
    {
        Dis = Vector3.Distance(Player.transform.position, transform.position);
        if (Dis < viewDist && !Dead&&See)
        {
            SentryBody.transform.LookAt(Player.transform.position);
            Timer -= 0.5f;
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
    private void ChackingSeePlayer() {
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
	public void Shooting(){
        Timer = ShootRate;
        Instantiate(Bullet,BulletPosition.transform.position,BulletPosition.transform.rotation);
	}
    void Rotation(float iTarget)
    {
        float roota = 0.0f;
        float rootb = 0.05f;
        transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, iTarget + 90,ref roota,rootb));
    }
}
