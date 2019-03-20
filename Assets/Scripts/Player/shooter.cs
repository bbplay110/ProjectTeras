using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shooter : MonoBehaviour  {

    public GameObject Gun;
    private RaycastHit shootHit;
    private RaycastHit AimHit;
    public float Damage=10;
    public float range = 20;
    private float tmpDistance;
    private bool isAimed = false;
    private Ray shootRay;
    private int shootableMask;
    private LineRenderer gunLine;
    [SerializeField]
    private GameObject mcamera;
    public float timeBetweenBullet=0.15f;
    private float effectDisplay = 0.2f;
    float timer;
    public Transform righthand;
    public GameObject thingtoAim;
    // Use this for initialization
    // Use this for initialization
    private void Awake()
    {
        mcamera = GameObject.Find("MainCamera");

        gunLine =Gun.GetComponent<LineRenderer>();
    }
    public void shoot() {
        timer = 0;
        gunLine.enabled = true;
        gunLine.SetPosition(0,Gun.transform.position);
        shootRay.origin = Gun.transform.position;
        shootRay.direction = Gun.transform.forward;
        if (Physics.Raycast(shootRay,out shootHit,range))
        {
            gunLine.SetPosition(1, shootHit.point);
            if (shootHit.transform.gameObject.GetComponent<hurt>() != null)
                shootHit.transform.gameObject.GetComponent<hurt>().damage(Damage, true);
            else if (shootHit.transform.GetComponent<DamageReciver>() != null)
                shootHit.transform.GetComponent<DamageReciver>().DoDamage(Damage);

            Debug.Log("shootThing");

            if (shootHit.collider.tag == "BreakableObject")
            {
                GameObject Box = shootHit.transform.gameObject;
                Box.GetComponent<Rigidbody>().AddForce(shootRay.direction*30,ForceMode.Force);
            }
            

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
    

    void Start () {
        thingtoAim = GameObject.Find("ThingToAim");

  	}
    void DisableEffects()
    {
        gunLine.enabled = false;

    }
    private void OnEnable()
    {
        
    }
    private void aim()
    {
        int handLayerIndex;
        handLayerIndex = GetComponent<Player>().Player1.GetLayerIndex("Hand");


        if (hInput.GetButtonDown("Aim"))
        {

            
            tmpDistance = mcamera.GetComponent<Camera3rdControl>().distence;
            GetComponent<Attacker>().enabled = false;
            GetComponent<Player>().Player1.SetLayerWeight(handLayerIndex, 1);
            thingtoAim.SetActive(true);
            GetComponent<Player>().SetAim(true);
            isAimed = true;
            mcamera.GetComponent<Camera3rdControl>().distence = 3;
        }
        else if (hInput.GetButton("Aim"))
        {
            shootRay.origin = Gun.transform.position;
            shootRay.direction = Gun.transform.forward;
            Physics.Raycast(shootRay, out AimHit, Mathf.Infinity);
            thingtoAim.transform.position = Camera.main.WorldToScreenPoint(AimHit.point);

            Debug.Log(AimHit.point);
        }
        else if (hInput.GetButtonUp("Aim"))
        {
            GetComponent<Attacker>().enabled = true;
            GetComponent<Player>().Player1.SetLayerWeight(handLayerIndex,0);
            thingtoAim.SetActive(false);
            mcamera.GetComponent<Camera3rdControl>().distence = tmpDistance;
            isAimed = false;
            GetComponent<Player>().SetAim(false);
        }

    }
    // Update is called once per frame
    void Update () {
        aim();
        timer += Time.deltaTime;
        Gun.transform.rotation = mcamera.transform.rotation;
        Gun.transform.position = righthand.position;
        if (hInput.GetButtonDown("Fire1")&&isAimed)
        {
            gameObject.GetComponent<Player>().Player1.SetBool("Fire", true);
        }
        else if (hInput.GetButtonUp("Fire1")||isAimed==false) {
            gameObject.GetComponent<Player>().Player1.SetBool("Fire", false);
        }
        if (timer >= timeBetweenBullet * effectDisplay) {
            DisableEffects();
        }
	}
    void save()
    {
    }
}
