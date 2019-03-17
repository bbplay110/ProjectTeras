using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciver : MonoBehaviour {
    private Transform parent;
	// Use this for initialization
	void Start () {
        parent = transform.parent;
	}
	public void DoDamage(float Damage)
    {
        Debug.Log("Bang!");
        while (parent.GetComponent <hurt>() == null)
        {
            parent = parent.transform.parent;
        }
        if(parent.GetComponent<hurt>()!=null)
            parent.GetComponent<hurt>().damage(Damage,false);
    }
	// Update is called once per frame
	void Update () {
		
	}

}
