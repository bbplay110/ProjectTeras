using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour {
    public GameObject IronBall;
    public GameObject HpSet;
    public bool turnWithPlayer;
    private bool tempTurnWithPlayer;
    public GameObject Brother;
    private Animator BossAnimator;
    private NavMeshAgent Agent;
    private Transform Player;
    private bool isPause=false;

    // Use this for initialization
   
    void Start()
    {
        //GetComponent<hurt>().Win = GameObject.Find("Wining");
        InvokeRepeating("CallBrother",0,3);
        BossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
    }
    public void SetTurnOff()
    {
        iTween.MoveTo(gameObject,new Vector3(Player.position.x, transform.position.y-7.3f, Player.position.z),1);
        turnWithPlayer = false;
    }
    void onEnemyPause()
    {
        isPause = true;
        BossAnimator.speed = 0;
        //GetComponent<NavMeshAgent>().isStopped = true;
        iTween.Pause(gameObject);
        iTween.Pause(IronBall);
        tempTurnWithPlayer = turnWithPlayer;
        turnWithPlayer = false;
    }
    void unEnemyPause()
    {
        isPause = false;
        BossAnimator.speed = 1;
        //GetComponent<NavMeshAgent>().isStopped = false;
        iTween.Resume(gameObject);
        iTween.Resume(IronBall);
        turnWithPlayer =tempTurnWithPlayer;
    }
    public void SetTurnOn()
    {
        turnWithPlayer = true;
    }
    public void throwIronBall()
    {
        Vector3 PlayerPos = Player.position;
        IronBall.GetComponent<Boss2IronBal>().ballfly(new Vector3(PlayerPos.x,IronBall.transform.position.y-3,PlayerPos.z));
    }
    // Update is called once per frame
    void Update()
    {
        if (turnWithPlayer)
        {
            Vector3 targetPostition = new Vector3(Player.position.x,
                     this.transform.position.y,
                     Player.position.z);
            this.transform.LookAt(targetPostition);
        }
    }
    private void OnDestroy()
    {
        Destroy(IronBall, 0);

        HpSet.SetActive(false);
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;

    }
    private void OnEnable()
    {
        if (Brother.activeSelf==true)
        {
            Brother.SetActive(false);
        }
        HpSet.SetActive(true);
    }
    void CallBrother()
    {
        float HpNow;
        HpNow = GetComponent<hurt>().HpBar.fillAmount;
        if (HpNow <= 0.5)
        {
            Brother.SetActive(true);
            CancelInvoke("CallBrother");
            
        }
    }
}
