using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class patrolMonster : MonoBehaviour
{
    public float WaitTime = 3;
    public Transform[] Points;

    public Vector3 IkLookOffect=new Vector3(0,4,0);
    private int destpoint = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private Ray Eye;
    public Transform Eyeball;
    public bool CanBrandingBrothers=false;
    public GameObject[] Brothers;
    
    private float LookWeight=0;
    
    private GameObject Player;

    
    private bool See, Dead = false;
    private float attackDist, viewDist;
    private float Dis;
    private bool isPause = false;
    // Use this for initialization
    void Start()
    {

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        attackDist = agent.stoppingDistance;
        viewDist = attackDist*10;
        //agent.autoBraking = false;
        if(!See&& Points.Length != 0)
            GoToNextPoint();
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
    }
    void onEnemyPause()
    {
        isPause = true;
        animator.speed = 0;
        GetComponent<NavMeshAgent>().isStopped = true;
        viewDist = 0;
        attackDist = 0;
    }
    void unEnemyPause()
    {
        isPause = false;
        animator.speed = 1;
        GetComponent<NavMeshAgent>().isStopped = false;
        attackDist = GetComponent<NavMeshAgent>().stoppingDistance;
        viewDist = attackDist * 2;
    }
    void GoToNextPoint()
    {
        if (Points.Length != 0)
        { 
        agent.destination = Points[destpoint].position;
        animator.SetBool("Walk", true);
        agent.isStopped = false;
        destpoint = (destpoint + 1) % Points.Length;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Points.Length != 0)
        {
            Patrol();
        }
        if (!float.IsNaN(Dis))
        {
            FindPlayer();
        }
        if (float.IsNaN(Dis))
        {
            Debug.Log("DisIsNan");
        }
        Debug.DrawRay(Eyeball.position, Player.transform.position - Eyeball.position+new Vector3(0,2,0), Color.red,0.1f );
        //Debug.DrawLine(Eyeball.position, Player.transform.position - Eyeball.position, Color.red, viewDist);
        ChackingSeePlayer();

        Debug.Log("LookWeight" + LookWeight);

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
        else if(Dis <= attackDist * 2&&Dis>attackDist)
        {
            if (CanBrandingBrothers && Brothers != null)
            {
                for (int i = 0; i < Brothers.Length; i++)
                {
                    Brothers[i].GetComponent<EnemySpawn>().Spawn();
                }
            }
            See = true;
            GetComponent<NavMeshAgent>().destination = Player.transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            animator.SetBool("Walk", true);
        }
        else if (Dis < viewDist && Dis > attackDist * 2 && See)
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
        if (agent.remainingDistance <= 0.5f && See==false&& Points.Length != 0)
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
        Eye.direction = (Player.transform.position + IkLookOffect) - Eyeball.position;
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
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
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
    private void OnAnimatorIK(int layerIndex)
    {

        animator.SetLookAtWeight(LookWeight);
        if (Dis <= attackDist) {
            LookWeight = 1;
            animator.SetLookAtPosition(Player.transform.position+IkLookOffect);
        }
        else if (Dis <= attackDist * 2 && Dis > attackDist)
        {
            LookWeight = 1;
            animator.SetLookAtPosition(Player.transform.position+IkLookOffect);
        }

        else if (Dis < viewDist &&Dis> attackDist * 2 && See)
        {
            LookWeight = 1;
            animator.SetLookAtPosition(Player.transform.position+IkLookOffect);
        }

        else if(Dis>=viewDist)
        {
            LookWeight = 0;
        }
    }

}
