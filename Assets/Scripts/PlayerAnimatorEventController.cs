using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEventController : MonoBehaviour {
    private Animator animator;
    public GameObject parents;
    public Transform leftFoot, rightFoot;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        parents = transform.parent.gameObject;
    }
    
    public void setJetOn() {
        if (parents.GetComponent<Player>().jet == 0)
        {
            parents.GetComponent<Player>().SetJet(false);

        }
        else
        {
            parents.GetComponent<Player>().SetJet(true);
        }
    }
    public void fakeRootMotion(float Distance)
    {
        
        float InputX = hInput.GetAxis("Horizontal");
        float InputY = hInput.GetAxis("Vertical");
        float rotate = Mathf.Atan2(InputY,-InputX)*Mathf.Rad2Deg;
        if (InputX != 0 && InputY != 0)
            iTween.RotateTo(gameObject, new Vector3(0, rotate - 90 + Camera.main.transform.eulerAngles.y, 0), 0.5f);
        if(parents.GetComponent<CharacterController>().isGrounded)
            iTween.MoveTo(parents, (transform.forward * Distance) + transform.position + new Vector3(0, 2.05f, 0), 0.5f);
        
    }
    public void attackTriggerOn()
    {
        parents.GetComponent<Attacker>().triggeron();
    }
    public void AttackFX(int FX)
    {
        parents.GetComponent<Attacker>().instantiateFX(FX);
    }
    public void shoot (){
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
        
    }
    // Update is called once per frame
    void Update () {
       /* RaycastHit left;
        if (Physics.Raycast(leftFoot.position,-leftFoot.up, out left, 0.1f)) {
            //animator.set
        }*/
	}
}
