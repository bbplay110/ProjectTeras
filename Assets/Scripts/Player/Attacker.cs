using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public Transform fxPoint;
    private bool root;
    public GameObject[] FX;
    private CharacterController player;
    public Animator Player2;
    private int stateLayer;
    //定义玩家连击次数  
    private int hit;
    private bool canTrigger = true;
    
    //定义状态常量值，前面不要带层名啊，否则无法判断动画状态  
    private const string IdleState = "Hit0";
    //private const string WalkState = "Walking";
    private const string AttackState1 = "Hit1";
    private const string AttackState2 = "Hit2";
    private const string AttackState3 = "Hit3";
    private const string SpinAttack = "Hurricane Kick";
    [SerializeField]
    // private bool waitForComboEnd=false; 

    //动画状态信息  
    private AnimatorStateInfo mStateInfo;
    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
    }
    private void Awake()
    {
        //Player=GetComponent
        stateLayer = Player2.GetLayerIndex("Base Layer");
        mStateInfo = Player2.GetCurrentAnimatorStateInfo(stateLayer);
    }
    // Update is called once per frame
    void Update()
    {

        mStateInfo = Player2.GetCurrentAnimatorStateInfo(stateLayer);
        if (mStateInfo.IsTag("hurt"))
        {
            ComboEnd();
        }
        if (canTrigger && hInput.GetButtonDown("Fire1"))
        {
            Attack();
        }
        checkComboEnd();
    }
    void checkComboEnd()
    {
        if (mStateInfo.normalizedTime >= 0.9f)
        {
            if (mStateInfo.IsTag(AttackState1) && Player2.GetInteger("AttackINT") == 1)
                ComboEnd();
            else if (mStateInfo.IsTag(AttackState2) && Player2.GetInteger("AttackINT") == 2)
                ComboEnd();
            else if (mStateInfo.IsTag(AttackState3) && Player2.GetInteger("AttackINT") == 3)
                ComboEnd();
        }
    }
    public void instantiateFX(int ELEMENT)
    {
        Instantiate(FX[ELEMENT], fxPoint.position, fxPoint.rotation);
    }
    public void triggeron()
    {
        canTrigger = true;
    }
    public void ComboEnd()
    {
        // mStateInfo = Player2.GetCurrentAnimatorStateInfo(stateLayer);
        GetComponent<Player>().SetTurn(true);
        GetComponent<Player>().setWalk(true);
        canTrigger = true;

        Player2.SetInteger("AttackINT", 0);
    }
    private void AirAttack()
    {
        //mStateInfo = Player2.GetCurrentAnimatorStateInfo(stateLayer);
        if (mStateInfo.IsTag(IdleState) && Player2.GetInteger("AttackINT") == 0)
        {
            // GetComponent<Player>().setWalk(false);
            Player2.SetInteger("AttackINT", 1);
            canTrigger = false;
        }
    }
    private void Attack()
    {
        if (mStateInfo.IsTag(IdleState))
        {

            if (GetComponent<CharacterController>().isGrounded)
                GetComponent<Player>().setWalk(false);
            GetComponent<Player>().SetTurn(false);
            canTrigger = false;
            Player2.SetInteger("AttackINT", 1);
        }
        if (mStateInfo.normalizedTime > 0.2f && mStateInfo.normalizedTime < 0.9f)
        {
            //获取状态信息  
            //如果玩家处于Idle状态且攻击次数为0，则玩家按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击  

            if (mStateInfo.IsTag(AttackState1))
            {

                GetComponent<Player>().SetTurn(false);


                if (GetComponent<CharacterController>().isGrounded)
                    GetComponent<Player>().setWalk(false);
                

                canTrigger = false;
                Player2.SetInteger("AttackINT", 2);
            }
            else if (mStateInfo.IsTag(AttackState2))
            {

                if (GetComponent<CharacterController>().isGrounded)
                    GetComponent<Player>().setWalk(false);
                GetComponent<Player>().SetTurn(false);
                canTrigger = false;
                Player2.SetInteger("AttackINT", 3);
            }
            else if (mStateInfo.IsTag(AttackState3))
            {

                if(GetComponent<CharacterController>().isGrounded)
                    GetComponent<Player>().setWalk(false);
                GetComponent<Player>().SetTurn(false);
                canTrigger = false;
                Player2.SetInteger("AttackINT", 1);
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (gameObject.GetComponent<CharacterController>().isGrounded && (Player2.GetInteger("AttackINT") >= 1 && Player2.GetInteger("AttackINT") <= 3))
        {
            GetComponent<Player>().setWalk(false);
        }
    }
}
