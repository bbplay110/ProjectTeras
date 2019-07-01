using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEventController : MonoBehaviour {
    private Animator animator;
    public GameObject parents;
    public Transform leftFoot, rightFoot;
    public AudioClip[] sfx;
    public Vector3 rootMoveDirction;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        //parents = transform.parent.gameObject;
    }

    public void setJetOn() {
        if (GetComponent<Player>().Energy == 0)
        {
           GetComponent<Player>().SetJet(false);

        }
        else
        {
            GetComponent<Player>().SetJet(true);
        }
    }
    public void fakeRootMotion()
    {
      GetComponent<Player>().rootMove(rootMoveDirction);
       // Debug.Log("MoveDirctionIs" + rootMoveDirction);
    }
    public void attackTriggerOn()
    {
        GetComponent<Attacker>().triggeron();
        animator.ResetTrigger("Jump");
    }
    public void AttackFX(int FX)
    {
        GetComponent<Attacker>().instantiateFX(FX);
    }
    public void shoot() {
        GetComponent<shooter>().shoot();
    }
    public void setJetOff()
    {
        GetComponent<Player>().SetJet(false);
    }
    public void SetHurtOn()
    {
        GetComponent<hurt>().SetHurton();
    }
    public void freezefalse()
    {
        GetComponent<Player>().SetTurn(true);
        GetComponent<Player>().setWalk(true);
        
    }
    public void freezerue()
    {
        GetComponent<Player>().SetTurn(false);
        GetComponent<Player>().setWalk(false);

        animator.SetInteger("AttackINT", 0);

    }
    //在Animation中發出聲音的事件
    public void PlaySfx(int sfxIndex)
    {
        AudioSource sfxOuput = GetComponent<AudioSource>();
        if (sfxIndex <=sfx.Length && GetComponent<CharacterController>().isGrounded)
        {
            sfxOuput.PlayOneShot(sfx[sfxIndex]);
        }

    }
    // Update is called once per frame
    void Update () {
       /* RaycastHit left;
        if (Physics.Raycast(leftFoot.position,-leftFoot.up, out left, 0.1f)) {
            //animator.set
        }*/
	}
}
