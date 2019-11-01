﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shooter : MonoBehaviour {
    //子彈射出的位置

    private bool TimePaused;
    public GameObject Gun;
    public GameObject[] GunModel;
    private int waponNow=0;
    private RaycastHit shootHit;
    private RaycastHit AimHit;
    public float Damage=10;
    public float range = 20;
    private float tmpDistance;
    private bool isAimed = false;
    private bool canFire = true;
    private Ray shootRay;
    private int shootableMask;
    private LineRenderer gunLine;
    public ParticleSystem GunHitPartical;
    [SerializeField]
    public GameObject BulletOnPaused;
    private GameObject mcamera;
    public float timeBetweenBullet=0.3f;
    private float effectDisplay = 0.2f;
    float timer;
    private Transform LeftHandIK, RightHandIK;
    private bool setIK = false;
    //public Transform righthand;
    public GameObject thingtoAim;
    public delegate void OnAim();
    public static event OnAim onAim;

    public delegate void UnAim();
    public static event UnAim unAim;
    private ParticleSystem gunFire;
    // Use this for initialization
    // Use this for initialization
    private Animator animator;
    public GameObject arrow;

    public int WaponNow
    {
        get
        {
            return waponNow;
        }

        set
        {
            waponNow = value;
        }
    }

    private void Awake()
    {
        mcamera = GameObject.Find("MainCamera");
        animator = gameObject.GetComponent<Animator>();
        gunLine =GetComponent<LineRenderer>();
        gunFire = Gun.GetComponent<ParticleSystem>();
        //GunHitPartical = Gun.transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    public void shoot() {
        if (waponNow == 0)
        {
            timer = 0;
            timeBetweenBullet = 0.3f;
            if (TimePaused)

            {
                GameObject tempBullet = Instantiate(BulletOnPaused, Gun.transform.position, Gun.transform.rotation, null);
                tempBullet.GetComponent<pistolBulletOnTimeStop>().Damage = Damage;
            }
            //shootRay.direction = Gun.transform.forward;
            else if (!TimePaused)
            {
                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.InverseTransformPoint(Gun.transform.position));
                gunFire.Play();
                if (Physics.Raycast(shootRay, out shootHit, range))
                {
                    gunLine.SetPosition(1, transform.InverseTransformPoint(shootHit.point));
                    if (shootHit.transform.gameObject.GetComponent<hurt>() != null&&shootHit.collider.tag!="Player")
                        shootHit.transform.gameObject.GetComponent<hurt>().damage(Damage, true);
                    else if (shootHit.transform.GetComponent<DamageReciver>() != null&&shootHit.collider.tag!="Player")
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
            }
        }
        else if (waponNow == 1)
        {
            
            GameObject tempArrow= Instantiate(arrow, Gun.transform.position,Gun.transform.rotation, null);
            GunModel[1].GetComponent<Animator>().SetTrigger("Shoot");
            timer = 0;
            timeBetweenBullet =0.7f;
            canFire = true;
            if (TimePaused)
            {
                tempArrow.GetComponent<Rigidbody>().Sleep();
            }
        }
       
    }
    

    void Start () {
        bulletTime.OnPauseTime += onTimePause;
        bulletTime.UnPauseTime += unTimePause;
        Debug.Log("MainCameraIs" + Camera.main.gameObject.name);    
        thingtoAim = GameObject.Find("ThingToAim");
        thingtoAim.SetActive(false);
        onAim += showGun;
        unAim += hideGun;
  	}
    void DisableEffects()
    {
        gunLine.enabled = false;
        //animator.ResetTrigger("FireSingle");

    }
    private void OnDestroy()
    {
        onAim -= showGun;
        unAim -= hideGun;
    }
    private void aim()
    {

        int handLayerIndex;
        handLayerIndex = animator.GetLayerIndex("Hand");
        if (hInput.GetButtonDown("Aim"))
        {
            tmpDistance = mcamera.GetComponent<Camera3rdControl>().distence;
            GetComponent<Attacker>().enabled = false;
            animator.SetLayerWeight(handLayerIndex, 1);
            thingtoAim.SetActive(true);
            GetComponent<Player>().SetAim(true);
            isAimed = true;
            mcamera.GetComponent<Camera3rdControl>().distence = 3;
            if(onAim!=null)
                onAim();

        }

        else if (hInput.GetButton("Aim"))
        {

            shootRay.origin = Gun.transform.position;
            Ray AimRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            //Gun.transform.position = righthand.position;
            if (Physics.Raycast(AimRay, out AimHit, Mathf.Infinity))
            {

                
                shootRay.direction = AimHit.point - Gun.transform.position;
                thingtoAim.transform.position = Camera.main.WorldToScreenPoint(AimHit.point);
                Debug.DrawLine(AimRay.origin,AimHit.point,Color.blue);
                //Debug.Log("AimThing!");
            }
            else
            {
                //Debug.Log("AimNoThing!");
                shootRay.direction = transform.forward;
            }
            //Physics.Raycast(shootRay, out AimHit, Mathf.Infinity);
            //thingtoAim.transform.position = Camera.main.WorldToScreenPoint(AimHit.point);

            //Debug.Log(AimHit.point);
        }

        else if (hInput.GetButtonUp("Aim"))
        {
            GetComponent<Attacker>().enabled = true;
            animator.SetLayerWeight(handLayerIndex,0);
            thingtoAim.SetActive(false);
            mcamera.GetComponent<Camera3rdControl>().distence = tmpDistance;
            isAimed = false;
            
            GetComponent<Player>().SetAim(false);
            if(unAim!=null)
                unAim();
        }

    }
    void showGun()
    {
        Debug.Log("ShowGun");
        foreach (var item in GunModel)
        {
            
            item.SetActive(false);
        }

        GunModel[WaponNow].SetActive(true);
        Debug.Log(GunModel[WaponNow].activeSelf);
        if(GunModel[WaponNow].transform.Find("LeftHandIK")!=null&& GunModel[WaponNow].transform.Find("RightHandIK")!=null)
        {
            LeftHandIK = GunModel[WaponNow].transform.Find("LeftHandIK");
            RightHandIK = GunModel[WaponNow].transform.Find("RightHandIK");
            setIK = true;
        }
    }
    void hideGun()
    {
        timer = 0;
        Debug.Log("HideGun");
        foreach (var item in GunModel)
        {
            item.SetActive(false);
        }
        setIK = false;
    }
    void onTimePause()
    {
        TimePaused = true;
    }
    void unTimePause()
    {
        TimePaused = false;
    }
    // Update is called once per frame
    void Update () {
        aim();
        timer += Time.deltaTime;
        //Debug.Log(timer);
        Gun.transform.rotation = mcamera.transform.rotation;
        if (hInput.GetButtonDown("Fire1")&&isAimed)
        {
            if (waponNow==0) {
                animator.SetBool("Fire", true);
            }
            else if (waponNow == 1&& canFire)
            {
                canFire = false;
                animator.SetTrigger("FireSingle");
            }
        }
        else if (hInput.GetButtonUp("Fire1")||isAimed==false) {
            if (waponNow != 1)
            {
                animator.SetBool("Fire", false);
            }
        }
        if (timer >= timeBetweenBullet * effectDisplay) {
            DisableEffects();
        }
	}
    private void OnAnimatorIK(int layerIndex)
    {
        if (setIK)
        {

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            
            animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.position);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandIK.position);

        }
        else
        {

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
