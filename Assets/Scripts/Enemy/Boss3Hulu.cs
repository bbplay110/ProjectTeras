using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Hulu : MonoBehaviour {
    public float TotalExistTime=15;
    private float HpHealth;
    private GameObject parent;
    private Animator animator;
	// Use this for initialization
	void Start () {
        parent = FindObjectOfType<Boss3>().gameObject;
        animator = parent.GetComponent<Animator>();

        HpHealth = parent.GetComponent<hurt>().TotalHP * 0.15f;
        Destroy(gameObject, TotalExistTime);
	}
	
	// Update is called once per frame
	void Update () {
        
        parent.GetComponent<hurt>().damage(-HpHealth/(TotalExistTime*Time.deltaTime*3600),true);
	}
    private void OnDestroy()
    {
        if(GetComponent<hurt>().HP1==0)
            animator.SetTrigger("Stun");
    }
}
