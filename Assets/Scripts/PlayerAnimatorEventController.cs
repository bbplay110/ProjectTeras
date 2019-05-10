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
        parents = transform.parent.gameObject;
    }

    public void setJetOn() {
        if (parents.GetComponent<Player>().energy == 0)
        {
            parents.GetComponent<Player>().SetJet(false);

        }
        else
        {
            parents.GetComponent<Player>().SetJet(true);
        }
    }
    public void fakeRootMotion()
    {
      parents.GetComponent<Player>().rootMove(rootMoveDirction);
        Debug.Log("MoveDirctionIs" + rootMoveDirction);
    }
    public void attackTriggerOn()
    {
        parents.GetComponent<Attacker>().triggeron();
    }
    public void AttackFX(int FX)
    {
        parents.GetComponent<Attacker>().instantiateFX(FX);
    }
    public void shoot() {
        parents.GetComponent<shooter>().shoot();
    }
    public void setJetOff()
    {
        parents.GetComponent<Player>().SetJet(false);
    }
    public void SetHurtOn()
    {
        parents.GetComponent<hurt>().SetHurton();
    }
    public void freezefalse()
    {
        parents.GetComponent<Player>().SetTurn(true);
        parents.GetComponent<Player>().setWalk(true);
        
    }
    public void freezerue()
    {
        parents.GetComponent<Player>().SetTurn(false);
        parents.GetComponent<Player>().setWalk(false);

        animator.SetInteger("AttackINT", 0);

    }
    public void PlaySfx(int sfxIndex)
    {
        AudioSource sfxOuput = GetComponent<AudioSource>();
        if (sfxIndex <= 2 && parents.GetComponent<CharacterController>().isGrounded)
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
