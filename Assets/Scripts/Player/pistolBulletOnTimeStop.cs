using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolBulletOnTimeStop : MonoBehaviour {
    private LineRenderer gunLine;
    float range = 50;
    private Ray shootRay;
    private RaycastHit shootHit;
    public ParticleSystem GunHitPartical;
    public ParticleSystem ShootParticle;
    public float Damage;
    // Use this for initialization
    void Start () {
        Invoke("pauseShootParticle",0.4f);
        gunLine = GetComponent<LineRenderer>();
        bulletTime.UnPauseTime += unTimeStop;
	}
    void pauseShootParticle()
    {
        ShootParticle.Pause(true);
    }
    void unTimeStop()
    {
        gunLine.enabled = true;
        ShootParticle.Play(true);
        gunLine.SetPosition(0, new Vector3(0,0,0));

        shootRay.origin = transform.position;
        shootRay.direction =transform.forward;
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
            GunHitPartical.Play();
        }
        else
        {
            gunLine.SetPosition(1, transform.InverseTransformPoint(shootRay.origin + shootRay.direction * range));
        }
        Destroy(gameObject, 0.1f);
        Debug.Log(gameObject.name);
    }
    private void OnDestroy()
    {
        bulletTime.UnPauseTime -= unTimeStop;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
