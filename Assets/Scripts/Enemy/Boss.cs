using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public GameObject HpSet;
    public bool turnWithPlayer;
    public bool look=false;
    private bool HandIKActive;
    
    private Animator BossAnimator;
    public float rightHandIKWeight=0;
    private Transform Player;
    public GameObject IKAim;
    public GameObject oraoraLeftHand,oraoraRightHand;
    private Transform PlayerCamera;
    public AudioClip BGM;

    //--時停變數
    private bool isPause = false;
    private Animator Ani;

    //--
    // Use this for initialization
    private void Awake()
    {
        PlayerCamera = FindObjectOfType<Camera3rdControl>().transform;

    }
    void Start () {
        BossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
        //--時停初始化
        bulletTime.OnPauseTime += onEnemyPause;
        bulletTime.UnPauseTime += unEnemyPause;
        //--
    }
    void onEnemyPause()
    {
        isPause = true;
        BossAnimator.speed = 0;
        turnWithPlayer = false;

    }
    void unEnemyPause()
    {
        isPause = false;
        BossAnimator.speed = 1;
        turnWithPlayer = true;
    }
    public void oraorashoot(int point) {
        switch (point) {
            case 0:
                oraoraLeftHand.GetComponent<BulletArray>().shoot();
                oraoraLeftHand.transform.Rotate(new Vector3(0,11.25f,0));
                break;
            case 1:
                oraoraRightHand.GetComponent<BulletArray>().shoot();
                oraoraRightHand.transform.Rotate(new Vector3(0, 11.25f, 0));
                break;
                
            default:
                Debug.Log("only_take_0_or_1");
                break;

        }
        
    }   
	// Update is called once per frame
	void Update () {
        if (turnWithPlayer) {
            Vector3 targetPostition = new Vector3(Player.position.x,
                     this.transform.position.y,
                     Player.position.z);
            this.transform.LookAt(targetPostition);
        }
        if (turnWithPlayer)
        {
            IKAim.transform.position = new Vector3(Player.transform.position.x,IKAim.transform.position.y, Player.transform.position.z);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        BossAnimator.SetIKPosition(AvatarIKGoal.RightHand,IKAim.transform.position);
        BossAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandIKWeight);
    }
    private void OnEnable()
    {
        HpSet.SetActive(true);
        //CanControllCamera = true;

        shooter.onAim += OnPlayerAim;
        shooter.unAim += OnPlayerNotAim;
        PlayerCamera.gameObject.GetComponent<AudioSource>().clip = BGM;
        PlayerCamera.gameObject.GetComponent<AudioSource>().Play();
    }
    public void lookRobot()
    {
        //CanControllCamera = true;
    }
    public void dontLookRobot()
    {
        FindObjectOfType<Camera3rdControl>().distence = 8;
    }
    void OnPlayerAim()
    {
    }
    void OnPlayerNotAim()
    {
    }

    private void OnDisable()
    {
        
        shooter.onAim -= OnPlayerAim;
        shooter.unAim -= OnPlayerNotAim;
    }
    private void OnDestroy()
    {
        bulletTime.OnPauseTime -= onEnemyPause;
        bulletTime.UnPauseTime -= unEnemyPause;
    }
    public void StartShake()
    {
        iTween.ShakePosition(gameObject,iTween.Hash("name","shake", "amount",new Vector3(5,5,5), "time",1f, "looptype",iTween.LoopType.pingPong,"easetype",iTween.EaseType.linear));
        GetComponent<Collider>().enabled = false;
        
        iTween.MoveTo(gameObject,iTween.Hash("position",gameObject.transform.position-new Vector3(0,65,0),"time",4,"delay",1));


       

        shooter.unAim -= OnPlayerNotAim;
    }
    public void StopShake()
    {
        iTween.StopByName("shake");
        GetComponent<Animator>().enabled = false;
        
    }
}
