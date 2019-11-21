using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : MonoBehaviour {
    public GameObject IronBall;
    public GameObject HpSet;
    public bool turnWithPlayer;
    public GameObject Brother;
    private Animator BossAnimator;
    private NavMeshAgent Agent;
    private Transform Player;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CallBrother",0,3);
        BossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }
    public void SetTurnOff()
    {
        iTween.MoveTo(gameObject,new Vector3(Player.position.x, transform.position.y-7.3f, Player.position.z),1);
        turnWithPlayer = false;
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
        

    }
    private void OnEnable()
    {
        if (Brother.active == true)
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
