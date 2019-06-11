using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolBullet : MonoBehaviour {
    private LineRenderer gunLine;
    float range = 20;
    private Ray shootRay;
    private RaycastHit shootHit;
    private ParticleSystem GunHitPartical;
    // Use this for initialization
    void Start () {
        gunLine = GetComponent<LineRenderer>();	
        
	}
	void unTimeStop()
    {
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.InverseTransformPoint(transform.position));

        //shootRay.origin = Gun.transform.position;
        //shootRay.direction = Gun.transform.forward;
        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            gunLine.SetPosition(1, transform.InverseTransformPoint(shootHit.point));
            if (shootHit.transform.gameObject.GetComponent<hurt>() != null)
                shootHit.transform.gameObject.GetComponent<hurt>().damage(Damage, true);
            else if (shootHit.transform.GetComponent<DamageReciver>() != null)
                shootHit.transform.GetComponent<DamageReciver>().DoDamage(Damage);

            Debug.Log("shootThing");
            GunHitPartical.transform.position = shootHit.point;
            GunHitPartical.transform.localRotation = Quaternion.FromToRotation(shootRay.origin, shootHit.point);
            if (shootHit.collider.tag == "BreakableObject")
            {
                GameObject Box = shootHit.transform.gameObject;
                Box.GetComponent<Rigidbody>().AddForce(shootRay.direction * 30, ForceMode.Force);
            }

        }
        else
        {
            gunLine.SetPosition(1, transform.InverseTransformPoint(shootRay.origin + shootRay.direction * range));
        }
        Destroy(gameObject, 0.1f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
