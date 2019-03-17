using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shootingEnemy : MonoBehaviour {
	private GameObject Player;
	private float attackDist,viewDist,Dis;
	private Animator Ani;
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
        attackDist = GetComponent<NavMeshAgent> ().stoppingDistance;
		viewDist = attackDist * 10;
		Ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        BulletPosition.transform.LookAt(Player.transform.position);
        FindPlayer();
        
        ChackingSeePlayer();
	}
    private void FindPlayer()
    {
        GetComponent<LineRenderer>().SetPosition(0,BulletPosition.transform.position);
        if (!See)
        {
            GetComponent<LineRenderer>().enabled = false;

        }
        GetComponent<LineRenderer>().SetPosition(1,Player.transform.position);
        Dis = Vector3.Distance(Player.transform.position, transform.position);
        if (Dis <= attackDist)
        {
            //BulletPosition.transform.LookAt(PlayerBody.position);
            Ani.SetBool("shooting", true);
            Ani.SetBool("walkforward", false);
            GetComponent<NavMeshAgent>().isStopped = true;
            
        }
        else if (Dis < viewDist && !Dead&&See)
        {
            GetComponent<NavMeshAgent>().destination = Player.transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            
            Ani.SetBool("walkforward", true);
        }
        else
        {
            if(!GetComponent<NavMeshAgent>().isStopped)
                GetComponent<NavMeshAgent>().isStopped = true;
            Ani.SetBool("walkforward", false);
            Ani.SetBool("shooting", false);
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
        GetComponent<NavMeshAgent>().isStopped = true;
    }
	public void Shooting(){
        Instantiate(Bullet,BulletPosition.transform.position,BulletPosition.transform.rotation);
	}
    void Rotation(float iTarget)
    {
        float roota = 0.0f;
        float rootb = 0.05f;
        transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, iTarget + 90,ref roota,rootb));
    }
}
