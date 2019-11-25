using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3 : MonoBehaviour {
    private Animator animator;
    public bool LeftHandIK, RightHandIK = false;
    public GameObject OutsideRing;
    public float LeftHandWeight, RightWeight = 1;
    public Transform leftHand;
    public GameObject Bomb;
    public GameObject JanQi;
    public Transform JanQiPosition;
    private Transform Player;
    private NavMeshAgent Agent;
    private float farAttackSequnce = 30;
    private float Dis, AttackDis, ViewDis, angle;
    private int animatorLayer;
    private AnimationState Drinking;
    private AnimationState ThrowBomb;
    private bool drinking1, drinking2, drinking3,spin = false;
    public GameObject Hulu;
    private bool isPause = false;
    // Use this for initialization

    void Start()
    {
        InvokeRepeating("LifeCheck",12,1);
        animator = GetComponent<Animator>();
        AttackDis = GetComponent<NavMeshAgent>().stoppingDistance;
        ViewDis = AttackDis * 3;
        Agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("checkDis", 10, 1);
        Agent.isStopped = true;
        animatorLayer = animator.GetLayerIndex("Base Layer");
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;

    }
    void onEnemyPause()
    {
        isPause = true;
        animator.speed = 0;
        GetComponent<NavMeshAgent>().isStopped = true;
        AttackDis = 0;
        ViewDis = 0;
    }
    void unEnemyPause()
    {
        isPause = false;
        animator.speed = 1;
        GetComponent<NavMeshAgent>().isStopped = false;
        AttackDis = GetComponent<NavMeshAgent>().stoppingDistance;
        ViewDis = AttackDis * 3;
    }
    // Update is called once per frame
    void Update()
    {
       
        Vector2 dirA = new Vector2(transform.forward.x, transform.forward.z);
        if (Player != null) { 
        Vector2 dirB = new Vector2(Player.position.x - transform.position.x, Player.position.z - transform.position.z);
        angle = Vector2.SignedAngle(dirA, dirB);
        animator.SetFloat("Angle", angle / 180);
        JanQiPosition.LookAt(Player.position, new Vector3(0, 0, 38).normalized);
        Dis = Vector3.Distance(Player.position, transform.position);
        Agent.destination = Player.position;
        }
        //Debug.Log("Angle" + angle);

    }
    public void KillerMode()
    {
        Player = GameObject.Find("Player").transform;
    }
    public void DrinkingHulu()
    {
        animator.SetTrigger("Drinking");
        GameObject tmpHuLu = Instantiate(Hulu,leftHand.position,Quaternion.Euler(0,0,90),leftHand)as GameObject;
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
            iTween.LookTo(gameObject, Player.position, 1);
            Agent.isStopped = true;
            if (Randomer > (100-farAttackSequnce))
            {
                animator.ResetTrigger("Meelee");
                animator.SetTrigger("Bomb");
            }
            else if (Randomer >= 30 && Randomer <= (100 - farAttackSequnce))
            {

            }
            else if (Randomer < 30)
            {
                animator.SetBool("Walk", true);
                Agent.isStopped = false;
            }

        }
        else if (Dis < AttackDis)
        {

            animator.SetBool("Walk", false);
            animator.SetTrigger("Meelee");
            animator.ResetTrigger("Bomb");
            Agent.isStopped = true;
        }

    }
    private void farAttack()
    {
        animator.SetBool("Walk", true);
        Agent.isStopped = false;
        int Randomer;
        Randomer = Random.Range(0, 100);
        if (Randomer > (100-farAttackSequnce)&&drinking1)
        {
            animator.SetTrigger("Roll");
            Agent.isStopped = true;

        }

    }
    public void DoSprint()
    {
        Vector3 target = new Vector3(Player.position.x, 0, Player.position.z);
        iTween.MoveTo(gameObject, transform.position + (target * 60), 1);


    }
    public void DoBomb()
    {
        GameObject tmpSpike = Instantiate(Bomb, leftHand.position, Quaternion.Euler(0, angle, 0),null) as GameObject;
        
        tmpSpike.GetComponent<Rigidbody>().AddForce(tmpSpike.transform.forward * 40);
        for (int i  = 0; i < 1; i++)
        {
            GameObject tmpBomb = Instantiate(Bomb, leftHand.position, Quaternion.Euler(0, angle, 0),null) as GameObject;
            tmpBomb.GetComponent<Rigidbody>().AddForce(new Vector3(tmpSpike.transform.forward.x+Random.Range(-20,20),0, tmpSpike.transform.forward.z+ Random.Range(-20, 20)) * 40);
        }

    }
    private void OnAnimatorIK(int layerIndex)
    {
    }
    void LifeCheck()
    {
        if ( GetComponent<hurt>().HP1 <= GetComponent<hurt>().TotalHP * 0.7f&& !drinking1)
        {
            drinking1 = true;
            DrinkingHulu();
        }
        else if(GetComponent<hurt>().HP1 <= GetComponent<hurt>().TotalHP * 0.4f&&!drinking2)
        {
            drinking2 = true;
            DrinkingHulu();
        }
        else if (GetComponent<hurt>().HP1 <= GetComponent<hurt>().TotalHP * 0.1f&&drinking3)
        {
            drinking3 = true;
            DrinkingHulu();
        }
        else if(GetComponent<hurt>().HP1 <= GetComponent<hurt>().TotalHP * 0.2f)
        {
            farAttackSequnce = 50;
        }

        else if (GetComponent<hurt>().HP1 <= GetComponent<hurt>().TotalHP * 0.5f && !spin)
        {
            animator.SetTrigger("Spin");
            Destroy(OutsideRing, 5);
            spin = true;
        }
        Debug.Log("checkLife!");

        Debug.Log("nextCheckIs"+GetComponent<hurt>().TotalHP * 0.7f);
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
    }
    public void DoSwordSlash(int count)
    {
        switch (count)
        {
            case 1:
                Instantiate(JanQi,JanQiPosition.position, JanQiPosition.rotation, null);
                break;
            case 2:
                Instantiate(JanQi, JanQiPosition.position, JanQiPosition.rotation, null);
                break;
            case 3:
                Instantiate(JanQi, JanQiPosition.position, JanQiPosition.rotation, null);
                Instantiate(JanQi, JanQiPosition.position, JanQiPosition.rotation, null);
                break;
            case 4:
                Instantiate(JanQi, JanQiPosition.position, Quaternion.Euler(0,-90,90));
                
                Instantiate(JanQi, JanQiPosition.position, Quaternion.Euler(0, 90, 90));

                break;
            case 5:
                Instantiate(JanQi, JanQiPosition.position, Quaternion.Euler(0, 0, 90));

                Instantiate(JanQi, JanQiPosition.position, Quaternion.Euler(0, 0, 90));
                break;
        }
    }
}
