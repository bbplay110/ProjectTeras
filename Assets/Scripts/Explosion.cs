using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour  {
    public string[] DamageTag;
    private int AttackableMask;
    public int LifeTime;
    public int Hurt;
    public bool CanDamageOtherThing=false; 
	// Use this for initialization
	void Start () {
        AttackableMask = LayerMask.GetMask("Enemy");
        //GetComponent<Rigidbody>().AddExplosionForce(30, transform.position,GetComponent<SphereCollider>().radius);
	}
	
	// Update is called once per frame
    
	void Update () {
        LifeTime -= 1;
        if (LifeTime <= 1)
        {
            Destroy(gameObject);

        }
	}
	void OnTriggerEnter(Collider Enemya){
        if (!CanDamageOtherThing) { 
        if (Enemya.tag != "Player")
        {
            if(Enemya.GetComponent<hurt>()!=null)
                Enemya.GetComponent<hurt>().damage(Hurt,true,transform);
            else if(Enemya.GetComponent<DamageReciver>() != null)
                {
                    Enemya.GetComponent<DamageReciver>().DoDamage(Hurt);
                }
        }
        }
        else
        {
            foreach (string Attackable in DamageTag)
                if (Enemya.gameObject.tag == Attackable)
                {
                    Debug.Log("hitThing!");
                    
                    Enemya.GetComponent<hurt>().damage(Hurt,true);
                    
                    Enemya.GetComponent<Animator>().SetTrigger("damage");

                    //Destroy(Enemya.gameObject);
                }

        }

        /*else if (Enemya.gameObject.layer == AttackableMask&&Enemya.tag=="BreakableObject")
        {
            Enemya.GetComponent<hurt>().damage(Hurt);
        }*/

    }
        
	
}
