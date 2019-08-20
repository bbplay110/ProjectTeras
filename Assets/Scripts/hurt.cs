using System.Collections;
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
    [HideInInspector]//https://dotblogs.com.tw/coolgamedevnote/2018/03/04/230939  <-關於[]內的東西是什麼的說明
    public bool HasExtraShild=false;
    [HideInInspector]
    public float TotalExtraShild = 0;
    [HideInInspector]
    public float ExtraShildRecover=1;
    [HideInInspector]
    public float ExtraShildRecoverTime = 3;
    [HideInInspector]
    private float CurrentShildRecoverTime;
    private float currentExtraShild;
    public Image ExtraShiledBar;
    public Image HpBar;
    public GameObject Win, Lose, mcamera=null;
    //public bool needCleared;
    public Transform UI;
    //public GameObject door;
    public Text HealthValue;
    public GameObject DeadBody;
    [HideInInspector]
    public GameObject hurtArrow;
    private int damageINT=0;

    public delegate void OnDiedEvent();
    public static OnDiedEvent onDied;
    private Animator animator;
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

    public float CurrentExtraShild
    {
        get
        {
            return currentExtraShild;
        }

        set
        {
            currentExtraShild = value;
        }
    }

    // Use this for initialization
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
        CurrentExtraShild = TotalExtraShild;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //HpBar.transform.LookAt(Camera.main.transform.position);
        if (damageINT >3)
        {
            damageINT = 0;
            animator.SetInteger("damageINT", 0);
        }

    }
    
    public void SetHurton()
    {
        CanHurt = true;
    }
    public void damage(float hurt,bool trap=false,Transform HurtPosition=null)
    {
        
        if (gameObject.tag == "Player")
        {
            //Debug.Log("canhurt=" + CanHurt);
        }
        if (gameObject.tag == "Player" && Wined == true)
        {
            hurt = 0;
        }
        if (CanHurt && CurrentExtraShild >= 0)
        {
            CurrentExtraShild -= hurt;
            if (TotalExtraShild >= 0 && ExtraShiledBar != null)
            {
                ExtraShiledBar.fillAmount = currentExtraShild / TotalExtraShild;
            }
        }
        else if (CanHurt&&currentExtraShild<=0)
        {

            HP1 -= hurt;
            Invoke("SetHurton", 0.3f);
            if(animator!=null)
                animator.SetTrigger("damage");
            if (gameObject.tag == "Player")
            {
                damageINT += 1;
                animator.SetInteger("damageINT",damageINT);
            }
            if (gameObject.tag == "Player"&&hurtArrow!=null&&HurtPosition!=null)
            {
                if (HealthValue != null)
                    
                    HealthValue.text = Mathf.RoundToInt((HP1 / TotalHP)*100)+"%" ;
                 GameObject tmpHurtArrow= Instantiate(hurtArrow,UI);
                float worldDeg = Vector2.SignedAngle(new Vector2(gameObject.GetComponent<Player>().cameraDir.forward.x, gameObject.GetComponent<Player>().cameraDir.forward.z),new Vector2( HurtPosition.position.x- gameObject.transform.position.x, HurtPosition.position.z - gameObject.transform.position.z));
                //tmpHurtArrow.GetComponent<RectTransform>().position = new Vector3(0,0,0);
                tmpHurtArrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,worldDeg);
                Destroy(tmpHurtArrow, 0.5f);
            }
        }

        if (!trap&&hurt>60) { 
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
                GetComponent<Collider>().enabled = false;
                }
                //if(needCleared)
                    //door.GetComponent<Door>().EnemysInSencer -= 1;
                if(onDied!=null)
                    onDied();
            }
            else if (gameObject.tag == "MiddleBoss")
            {
                //if(door!=null)
                    //door.GetComponent<Door>().EnemysInSencer -= 2;
                onDied();
            }
            else if (gameObject.tag == "Boss")
            {
                //whateverSaeaegeageadzgszhsxdyj
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().SetTrigger("death");
                    if(GetComponent<Collider>()!=null)
                    GetComponent<Collider>().enabled = false;
                }
                if(GetComponent<Collider>()!=null)
                    GetComponent<Collider>().enabled = false;
                if(Win!=null)
                    Win.SetActive(true);
                Invoke("CloseBar", 1);
                //Cursor.visible = true;
                Time.timeScale = 0.5f;
                
                //Wined = true;
                BossDeath(5);
                Destroy(gameObject, DeathTime);
                onDied();
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
        Invoke("loadNextLevel",3);
    }
    void loadNextLevel()
    {
        GetComponent<SceneLoad>().LoadScene(GetComponent<SceneLoad>().scenes);

    }
    private void Restart()
    {
        //SceneManager.LoadScene(RestartScene);
        SLMenu manager = FindObjectOfType<SLMenu>();
        manager.quickLoad();
    }
    private void OnGUI()
    {
        
    }
}
