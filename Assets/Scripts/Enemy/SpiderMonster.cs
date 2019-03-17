using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpiderMonster : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject Explosion;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private bool See, Dead = false;
    private float attackDist, viewDist;
    private float Dis;
    private bool touchGround= true;
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player_Body");
        //agent.autoBraking = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(touchGround)
            agent.destination = Player.transform.position;
        Ray rray=new Ray();
        rray.origin = transform.position;
        rray.direction = transform.position - Player.transform.position;
        RaycastHit Hit;
        Dis = Vector3.Distance(transform.position, Player.transform.position);
        if (Dis <= agent.stoppingDistance&&Physics.Raycast(rray,out Hit, 30))

        {
            if (Hit.transform.tag == "Player")
            {

                animator.SetBool("Explode", true);
            }
            else
            {

                animator.SetBool("Explode", true);
            }
        }
        else
        {
            animator.SetBool("Explode",false);
        }

    }
    public void isDeath()
    {
        Dead = true;
        GetComponent<NavMeshAgent>().isStopped = true;
    }
    public void Spawn() {
        GetComponent<BoxCollider>().enabled = true;
    }
    public void explode()
    {
        Instantiate(Explosion,transform.position,transform.rotation);
        Destroy(gameObject);

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (agent.enabled == false)
        {
            agent.enabled = true;
            agent.destination = Player.transform.position;
            touchGround = true;
            GetComponent<BoxCollider>().isTrigger = true;

        }
    }

}
