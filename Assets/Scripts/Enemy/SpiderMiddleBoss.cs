using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpiderMiddleBoss : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject smallSpider;
    public GameObject PPlayer;
    public Transform BirthPoint;
    private bool See, Dead = false;
    public float attackDist, viewDist;
    public float Dis;
    public GameObject VirusBomb;
    public bool trace=true;
    private bool isShoot=false;
    private float Timer , SWITCH = 0 ;
    // Use this for initialization
    void Start()
    {
        //BirthPoint = transform.Find("BirthPoint");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        PPlayer = GameObject.Find("Player_Body");
        attackDist = agent.stoppingDistance;
        viewDist = agent.stoppingDistance * 2;
        //agent.autoBraking = false;

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = PPlayer.transform.position;
        Dis = Vector3.Distance(transform.position, PPlayer.transform.position);
        if (Dis <= attackDist)

        {
                agent.isStopped = true;
                animator.SetBool("walk", false);
            
            animator.SetBool("Birth", false);
                animator.SetBool("Attack", true);

            CancelInvoke("Shoot");
        }
        else if (Dis < viewDist && Dis > attackDist)
        {

            animator.SetBool("Attack", false);
            animator.SetBool("walk", false);
            animator.SetBool("Birth", true);
            CancelInvoke("Shoot");
        }
        else if(Dis >= viewDist){

            animator.SetBool("Birth", false);

            animator.SetBool("Attack", false);
            if (trace) { 
            animator.SetBool("walk", true);
            agent.isStopped = false;
            }
            agent.SetDestination(PPlayer.transform.position);
            Shoot();
            
        }
        
        iTween.LookUpdate(BirthPoint.gameObject,PPlayer.transform.position,0.1f);

    }
    public void isDeath()
    {
        Dead = true;
        GetComponent<NavMeshAgent>().isStopped=true;
    }
    public void Spawn() {
        GetComponent<BoxCollider>().enabled = true;
    }
    void Shoot()
    {
        Timer += 1*Time.deltaTime;
        //Debug.Log("Timer=" + Timer);
        if (Timer >=1)
        {
            SWITCH = Random.Range(0, 100);
            //Debug.Log("Switch"+SWITCH);
            if (SWITCH > 40)
            {

                //Debug.Log("Shoottt");
                trace = false;
                agent.isStopped = true;
                animator.SetBool("walk", false);
                animator.SetTrigger("Shoot");


            }

            Timer = 0;
        }
        
    }
    public void ShootVirusBomb()
    {
        GameObject tmpVirusBomb = Instantiate(VirusBomb, BirthPoint.position, BirthPoint.rotation, null) as GameObject;
        tmpVirusBomb.GetComponent<Rigidbody>().AddForce((tmpVirusBomb.transform.forward + (tmpVirusBomb.transform.up / 2) )* Dis*20);
        trace = true;
    }
    public void Birth()
    {
        Instantiate(smallSpider,BirthPoint.position,transform.rotation);
    }


}
