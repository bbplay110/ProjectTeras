﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;


public class hurt : MonoBehaviour
{
    public delegate void DefeatBoss(int secand);
    public static event DefeatBoss BossDeath;
    public delegate void PlayerDeath();
    public float DeathTime=3;
    private string RestartScene;
    public float TotalHP = 100;
    private static float PlayerCurrentHealth;
    float HP;
    private bool Wined=false;
    private bool CanHurt = true;
    public Image HpBar;
    public GameObject Win, Lose, mcamera=null;
    public bool needCleared;
    public Transform UI;
    public GameObject door;
    public Text HealthValue;
    public GameObject DeadBody;
    public GameObject hurtArrow;
    public float HP1
    {
        get
        {
            return HP;
        }

        set
        {
            HP = value;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Scene mscene = SceneManager.GetActiveScene();
            RestartScene = mscene.name;
            Cursor.visible = false;
            Time.timeScale =1;
            Wined = false;
            UI = GameObject.Find("Main-UI").transform;
        }
        HP1 = TotalHP;
    }

    // Update is called once per frame
    void Update()
    {
        //HpBar.transform.LookAt(Camera.main.transform.position);
    }
    public void SetHurton()
    {
        CanHurt = true;
    }
    public void damage(float hurt,bool trap=false,Transform HurtPosition=null)
    {
        if (gameObject.tag == "Player" && Wined == true)
        {
            hurt = 0;
        }

        if (CanHurt)
        {
            HP1 -= hurt;
            Invoke("SetHurton", 0.3f);
            if (gameObject.tag == "Player"&&hurtArrow!=null&&HurtPosition!=null)
            {
                if (HealthValue != null)
                    HealthValue.text = (HP1 / TotalHP)*100+"%" ;
                 GameObject tmpHurtArrow= Instantiate(hurtArrow,UI);
                float worldDeg = Vector2.SignedAngle(new Vector2(gameObject.GetComponent<Player>().cameraDir.forward.x, gameObject.GetComponent<Player>().cameraDir.forward.z),new Vector2( HurtPosition.position.x- gameObject.transform.position.x, HurtPosition.position.z - gameObject.transform.position.z));
                //tmpHurtArrow.GetComponent<RectTransform>().position = new Vector3(0,0,0);
                tmpHurtArrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,worldDeg);
                Destroy(tmpHurtArrow, 0.5f);
            }
        }
        if (!trap) { 
            CanHurt = false;
            
        }
        if(HpBar!=null)
            HpBar.fillAmount = HP1 / TotalHP;
        if (HP1<=0)
        {

            if (gameObject.tag == "Player")
            {
                
                GetComponent<Player>().setWalk(false);
                GetComponent<CharacterController>().enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0.5f;
                Lose.SetActive(true);
                if (mcamera!= null)
                {
                    mcamera.GetComponent<Grayscale>().enabled = true;
                }
                Invoke("Restart", 2);
                GetComponent<Player>().enabled = false;

            }
            else if(gameObject.tag=="Enemy")
            {

                Destroy(gameObject, DeathTime);
                if(GetComponent<Animator>() != null) { 
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<CapsuleCollider>().enabled = false;
                }
                if(needCleared)

                    door.GetComponent<Door>().EnemysInSencer -= 1;

            }
            else if (gameObject.tag == "MiddleBoss")
            {
                if(door!=null)
                    door.GetComponent<Door>().EnemysInSencer -= 2;
            }
            else if (gameObject.tag == "Boss")
            {
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().SetTrigger("death");
                    if(GetComponent<CapsuleCollider>()!=null)
                    GetComponent<CapsuleCollider>().enabled = false;
                }
                if(GetComponent<CapsuleCollider>()!=null)
                    GetComponent<CapsuleCollider>().enabled = false;
                if(Win!=null)
                    Win.SetActive(true);
                Invoke("CloseBar", 1);
                //Cursor.visible = true;
                Time.timeScale = 0.5f;
                
                Wined = true;
                //BossDeath(5);
                Destroy(gameObject, DeathTime);
                
            }
            else 
            {
                Destroy(gameObject);
            }           
        }
    }
    
    void CloseBar()
    {
        Time.timeScale = 1;
        Win.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(RestartScene);

    }
    private void OnGUI()
    {
        
    }
}
