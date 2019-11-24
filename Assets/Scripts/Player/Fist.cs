using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    private GameObject BadGuyBody;
    public float Damage = 50;
    public string[] canAttack =new string[] {"Enemy", "Boss", "BreakableObject"};
    
    public static GameObject HitParticle;
    // Use this for initialization
    void Start()
    {
        
        //以玩家身上的attacker腳本中FX陣列的第0個物件
        HitParticle = GameObject.FindObjectOfType<Attacker>().FX[0];
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Pow!,Tag is"+collision.tag);
        //if(col)
        BadGuyBody = collision.gameObject;
        for (int i = 0; i < canAttack.Length; i++)
        {
            if (collision.gameObject.tag == canAttack[i])
            {
                Debug.Log("Punch!");
                if (collision.GetComponent<hurt>() != null)
                {
                    collision.GetComponent<hurt>().damage(Damage, false);
                }
                if (collision.GetComponent<DamageReciver>() != null)
                {
                    collision.GetComponent<DamageReciver>().DoDamage(Damage);
                }
                //collision.GetComponent<hurt>().damage(Damage);
                //if(BadGuyBody.GetComponent<Animator>()!=null)
                //BadGuyBody.GetComponent<Animator>().SetTrigger("damage");
                //GameObject tempHitParticle = Instantiate(HitParticle, collision.transform.position, collision.transform.rotation, null);
                //Destroy(tempHitParticle, 0.6f);
            }
        }


            
    }
}
