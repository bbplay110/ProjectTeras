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
    private void OnCollisionEnter(Collision collision)
    {
        BadGuyBody = collision.gameObject;
        foreach (var item in canAttack)
        {
            if (collision.gameObject.tag == item)
            {
                collision.gameObject.GetComponent<hurt>().damage(Damage);
                if(BadGuyBody.GetComponent<Animator>()!=null)
                    BadGuyBody.GetComponent<Animator>().SetTrigger("damage");
                GameObject tempHitParticle = Instantiate(HitParticle,collision.transform.position, collision.transform.rotation, null);
                Destroy(tempHitParticle, 0.6f);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
