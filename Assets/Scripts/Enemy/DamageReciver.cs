using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciver : MonoBehaviour {
    private Transform parent;
    public float DamageWeight = 1;
	// Use this for initialization
	void Start () {
        parent = transform.parent;
        while (parent.GetComponent<hurt>() == null)
        {
            parent = parent.transform.parent;
        }
    }
	public void DoDamage(float Damage)
    {
        Debug.Log("Bang!");

        if(parent.GetComponent<hurt>()!=null)
            parent.GetComponent<hurt>().damage(Damage*DamageWeight,false);
    }
	// Update is called once per frame
	void Update () {
		
	}

}
