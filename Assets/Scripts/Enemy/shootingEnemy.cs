using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shootingEnemy : MonoBehaviour
{
    
    
    private GameObject Player;
    public  float attackDist, viewDist, Dis;
    private Animator Ani;
    public GameObject Bullet;
    public GameObject BulletPosition;
    public Transform PlayerBody;
    private Ray eyeContect;
    private bool See = false;
    private bool Dead = false;
    public bool LookWithPlayer = true;
    private bool isPause = false;
    public Vector3 lookIkOffset = new Vector3(0, 4, 0);
    public Vector3 ChestOffect = new Vector3(0,-40,0);
    private Vector3 LaserPoint = new Vector3();
    private Vector3 Pos;
    private Transform Chest;
    // Use this for initialization

    void Start()
    {
        //--時停初始化
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
        Ani = GetComponent<Animator>();
        //--

        //this.GetComponent<AudioSource>().Play();
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
        if (GetComponent<LineRenderer>() != null)
        {
            GetComponent<LineRenderer>().useWorldSpace = true;
        }
        PlayerBody = GameObject.Find("Player").transform;
        Player = GameObject.Find("Player");
        attackDist = GetComponent<NavMeshAgent>().stoppingDistance;
        viewDist = attackDist * 2;
        Ani = GetComponent<Animator>();

        Chest = Ani.GetBoneTransform(HumanBodyBones.Chest);

        //Debug.Log(Chest.name);
    }

    void onEnemyPause()
    {
        isPause = true;
        Ani.speed = 0;
        GetComponent<NavMeshAgent>().isStopped = true;
        viewDist = 0;
        attackDist = 0;
    }
    void unEnemyPause()
    {
        isPause = false;
        Ani.speed = 1;
        GetComponent<NavMeshAgent>().isStopped = false;
        attackDist = GetComponent<NavMeshAgent>().stoppingDistance;
        viewDist = attackDist * 2;
    }

    // Update is called once per frame
    void Update()
    {
        Pos = ((PlayerBody.position + new Vector3(0, 2, 0))- (transform.position + transform.TransformDirection(0, 1.484f, 0))).normalized;
        ChackingSeePlayer();
        BulletPosition.transform.LookAt(PlayerBody.position + new Vector3(0, 2, 0));
        FindPlayer();

    }
    private void FindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(BulletPosition.transform.position, (PlayerBody.position + new Vector3(0, 2, 0)) - BulletPosition.transform.position, out hit, Mathf.Infinity) && !isPause)
        {
            LaserPoint = hit.point;
        }

        GetComponent<LineRenderer>().SetPosition(0, BulletPosition.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, LaserPoint);
        Dis = Vector3.Distance(Player.transform.position, transform.position);
        if (Dis <= attackDist)
        {
            //BulletPosition.transform.LookAt(PlayerBody.position);
            Ani.SetBool("shooting", true);
            Ani.SetBool("walkforward", false);
            GetComponent<NavMeshAgent>().isStopped = true;
            if (LookWithPlayer)
                iTween.LookUpdate(gameObject, iTween.Hash("looktarget", PlayerBody.position, "axis", "y", "time", 0.4f));
        }
        else if (Dis < viewDist && !Dead)
        {
            GetComponent<NavMeshAgent>().destination = PlayerBody.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            Ani.SetBool("walkforward", true);

        }
        else if (Dis > viewDist)
        {
            if (!GetComponent<NavMeshAgent>().isStopped)
                GetComponent<NavMeshAgent>().isStopped = true;
            
            Ani.SetBool("walkforward", false);
            Ani.SetBool("shooting", false);
        }
    }
    private void OnDisable()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
    }
    private void LateUpdate()
    {
        if (LookWithPlayer) { 
            if (Dis <= attackDist)
            {
                Chest.LookAt(PlayerBody.position + new Vector3(0, 2, 0));

                Chest.rotation = Chest.rotation * Quaternion.Euler(ChestOffect);
            }
            else if (Dis < viewDist && !Dead)
            {
                Chest.LookAt(PlayerBody.position + new Vector3(0, 2, 0));

                Chest.rotation = Chest.rotation * Quaternion.Euler(ChestOffect);

            }
            else if (Dis > viewDist)
            {
            }
        }
    }
    private void ChackingSeePlayer()
    {
        RaycastHit hit;
        eyeContect.origin = BulletPosition.transform.position;
        eyeContect.direction = PlayerBody.position - BulletPosition.transform.position;
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
        else
        {
            See = false;
        }
    }
    public void isDeath()
    {
        Dead = true;
        GetComponent<NavMeshAgent>().isStopped = true;
    }
    public void Shooting()
    {
        GameObject tmpBullet = Instantiate(Bullet, BulletPosition.transform.position, BulletPosition.transform.rotation);
        Physics.IgnoreCollision(tmpBullet.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
    }
    private void OnAnimatorIK(int layerIndex)
    {
        
        
        if (Dis <= attackDist)
        {
            Ani.SetLookAtWeight(1);
            Ani.SetLookAtPosition(PlayerBody.position+lookIkOffset);
            /*
            Ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            Ani.SetIKPosition(AvatarIKGoal.LeftHand, Pos );

            Ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            Ani.SetIKPosition(AvatarIKGoal.RightHand, Pos + transform.TransformDirection(0.087f, -0.058f, -0.338f));
            /*Ani.SetIKHintPositionWeight(AvatarIKHint.RightElbow,1);
            Ani.SetIKHintPosition(AvatarIKHint.RightElbow,PosR*5);

            Ani.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
            Ani.SetIKHintPosition(AvatarIKHint.LeftElbow, PosL * 2);*/
        }
        else if (Dis < viewDist && !Dead)
        {
            Ani.SetLookAtWeight(1);
            Ani.SetLookAtPosition(PlayerBody.position+lookIkOffset);
            /*
            Ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            Ani.SetIKPosition(AvatarIKGoal.LeftHand, Pos);

            Ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            Ani.SetIKPosition(AvatarIKGoal.RightHand, Pos+transform.TransformDirection(0.087f,-0.058f,-0.338f));
            /* Ani.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
             Ani.SetIKHintPosition(AvatarIKHint.RightElbow, PosR * 5);

             Ani.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
             Ani.SetIKHintPosition(AvatarIKHint.LeftElbow, PosL * 2);*/
        }
        else if (Dis > viewDist)
        {
            Ani.SetLookAtWeight(0);
            /*
            Ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            Ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 0); 
            /*Ani.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);

            Ani.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);*/
        }
    }
}