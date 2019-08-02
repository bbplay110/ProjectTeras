using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class patrolMonster : MonoBehaviour
{
    public float WaitTime = 3;
    public Transform[] Points;

    
    private int destpoint = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private Ray Eye;
    public Transform Eyeball;
    public bool CanBrandingBrothers=false;
    public GameObject[] Brothers;
    

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private bool See, Dead = false;
    private float attackDist, viewDist;
    private float Dis;
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        attackDist = agent.stoppingDistance;
        viewDist = attackDist*10;
        //agent.autoBraking = false;
        if(!See)
            GoToNextPoint();
    }
    void GoToNextPoint()
    {
        if (Points.Length == 0)
            return;
        agent.destination = Points[destpoint].position;
        animator.SetBool("Walk", true);
        agent.isStopped = false;
        destpoint = (destpoint + 1) % Points.Length;

    }
    // Update is called once per frame
    void Update()
    {
        Patrol();
        FindPlayer();
        Debug.DrawRay(Eyeball.position, Player.transform.position - Eyeball.position+new Vector3(0,2,0), Color.red,viewDist);
        //Debug.DrawLine(Eyeball.position, Player.transform.position - Eyeball.position, Color.red, viewDist);
        ChackingSeePlayer();

    }
    private void FindPlayer()
    {

        Dis = Vector3.Distance(Player.transform.position, Eyeball.position);
        if (Dis <= attackDist)
        {
            //BulletPosition.transform.LookAt(PlayerBody.position);
            animator.SetBool("shooting", true);
            animator.SetBool("Walk", false);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        else if (Dis < viewDist && !Dead && See)
        {
            if (CanBrandingBrothers&&Brothers!=null)
            {
                for(int i=0; i < Brothers.Length; i++)
                {
                    Brothers[i].GetComponent<EnemySpawn>().Spawn();
                }
            }
            See = true;
            GetComponent<NavMeshAgent>().destination = Player.transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            animator.SetBool("Walk", true);
        }
        else
        {
            See = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            animator.SetBool("Walk", false);
            animator.SetBool("shooting", false);
        }
    }
    private void Patrol()
    {
        if (agent.remainingDistance <= 0.5f && See==false&&Points!=null)
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            Invoke("GoToNextPoint", WaitTime);
        }
    }
    private void ChackingSeePlayer()
    {
        
        RaycastHit hit;
        Eye.origin = Eyeball.position;
        Eye.direction = Player.transform.position - Eyeball.position+new Vector3(0,2,0);
        //Debug.DrawLine(eyeContect.origin, eyeContect.GetPoint(viewDist),Color.red,viewDist,true);
        if (Physics.Raycast(Eye, out hit, viewDist))
        {
            if (hit.collider.tag == "Player")
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
    public void Shooting()
    {

    }
    public void Rotation()
    {
        iTween.LookTo(gameObject, iTween.Hash("looktarget", Player.transform.position, "axis", "y", "time", 0.4f));
        //iTween.RotateTo(gameObject, Player.transform.position, 0.4f);
    }

}
