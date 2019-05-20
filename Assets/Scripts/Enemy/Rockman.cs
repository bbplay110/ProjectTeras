using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rockman : MonoBehaviour {
    public GameObject Spike;
    private Transform Player;
    private NavMeshAgent Agent;
    private Animator animator;
    private float Dis, AttackDis, ViewDis,angle;
    private int animatorLayer;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        AttackDis = GetComponent<NavMeshAgent>().stoppingDistance;
        ViewDis = AttackDis * 3;
        Agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("checkDis",6,1);
        Agent.isStopped=true;
        animatorLayer = animator.GetLayerIndex("Base Layer");
        Player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Dis = Vector3.Distance(Player.position, transform.position);
        Vector2 dirA = new Vector2(transform.forward.x,transform.forward.z);
        Vector2 dirB = new Vector2(Player.position.x-transform.position.x, Player.position.z - transform.position.z);
        angle =Vector2.SignedAngle(dirA,dirB);
        animator.SetFloat("Angle", angle / 180);
        //Debug.Log("Angle" + angle);

        Agent.destination = Player.position;
	}
    void checkDis()
    {
        if (Dis > ViewDis)
        {

            farAttack();

        }
        else if (Dis > AttackDis && Dis < ViewDis)
        {
            animator.SetBool("Walk", false);
            int Randomer;
            Randomer = Random.Range(0, 100);
            iTween.LookTo(gameObject,Player.position,1);
            Agent.isStopped = true;
            if (Randomer > 70)
            {

                animator.SetTrigger("Spike");
            }
            if (Randomer < 30)
            {
                animator.SetBool("walk", true);
                Agent.isStopped = false;
            }

        }
        else if (Dis < AttackDis)
        {
            
            animator.SetBool("walk", false);
            animator.SetTrigger("Meelee");
            Agent.isStopped=true;
        }

    }
    private void farAttack()
    {
        animator.SetBool("Walk", true);
        Agent.isStopped = false;
        int Randomer;
        Randomer = Random.Range(0, 100);
        if (Randomer > 70)
        {
            animator.SetTrigger("Roll");
            Agent.isStopped=true;

        }
    }
    public void DoSprint()
    {
        Vector3 target = new Vector3(transform.forward.x,0,transform.forward.z);
        iTween.MoveTo(gameObject,transform.position+(target*60),1);

    }
    public void DoSpike()
    {
        GameObject tmpSpike= Instantiate(Spike, transform.position+(transform.forward*(Dis-5)), Quaternion.Euler(0,angle,0)) as GameObject;
        
        Destroy(tmpSpike, 7);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(animator.GetCurrentAnimatorStateInfo(animatorLayer).IsName("Dive Roll")&&other.tag!="Player")
        {
            Debug.Log("sTUN!");
            animator.SetTrigger("Stun");
        }
    }
}
