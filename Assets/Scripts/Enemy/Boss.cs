using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public GameObject HpSet;
    public bool turnWithPlayer;
    public bool look=false;
    private Animator BossAnimator;
    private Transform Player;
    public GameObject oraoraLeftHand,oraoraRightHand;
    private Transform PlayerCamera;
    private bool CanControllCamera;
    private Vector3 tmpCameraPoint;
    private float tmpCameraDistance;
    // Use this for initialization
    private void Awake()
    {
        PlayerCamera = FindObjectOfType<Camera3rdControl>().target;
    }
    void Start () {
        BossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
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
        
        CameraControll();
    }
    void CameraControll()
    {
        if (CanControllCamera)
        {
            PlayerCamera.position = ((Player.position + new Vector3(0, 2, 0)) + (transform.position + new Vector3(0, 50, 0))) / 2;
            FindObjectOfType<Camera3rdControl>().distence = Vector3.Distance(Player.position + new Vector3(0, 2, 0), transform.position + new Vector3(0, 50, 0)) + 8;
        }
    }
    private void OnEnable()
    {
        HpSet.SetActive(true);
        tmpCameraPoint = PlayerCamera.localPosition;
        tmpCameraDistance = FindObjectOfType<Camera3rdControl>().distence;
        FindObjectOfType<Camera3rdControl>().maxDistence = 300;
        

        //CanControllCamera = true;

        shooter.onAim += OnPlayerAim;
        shooter.unAim += OnPlayerNotAim;
    }
    public void lookRobot()
    {
        CanControllCamera = true;
    }
    public void dontLookRobot()
    {
        CanControllCamera = false;
        PlayerCamera.localPosition = tmpCameraPoint;
        FindObjectOfType<Camera3rdControl>().distence = 8;
    }
    void OnPlayerAim()
    {
        CanControllCamera = false;
        PlayerCamera.localPosition = tmpCameraPoint;
        FindObjectOfType<Camera3rdControl>().distence = 8;
    }
    void OnPlayerNotAim()
    {
        CanControllCamera = true;
        PlayerCamera.position = ((Player.position + new Vector3(0, 2, 0)) + (transform.position + new Vector3(0, 50, 0))) / 2;
    }

    private void OnDisable()
    {
        if (tmpCameraPoint != null)
            PlayerCamera.localPosition = tmpCameraPoint;
        FindObjectOfType<Camera3rdControl>().maxDistence = 8;
        FindObjectOfType<Camera3rdControl>().distence = tmpCameraDistance;

        shooter.onAim -= OnPlayerAim;
        shooter.unAim -= OnPlayerNotAim;
    }
    public void StartShake()
    {
        iTween.ShakePosition(gameObject,iTween.Hash("name","shake", "amount",new Vector3(5,5,5), "time",1f, "looptype",iTween.LoopType.pingPong,"easetype",iTween.EaseType.linear));
        GetComponent<Collider>().enabled = false;
        
        iTween.MoveTo(gameObject,iTween.Hash("position",gameObject.transform.position-new Vector3(0,65,0),"time",4,"delay",1));


        if (tmpCameraPoint != null)
            PlayerCamera.localPosition = tmpCameraPoint;
        FindObjectOfType<Camera3rdControl>().maxDistence = 8;
        FindObjectOfType<Camera3rdControl>().distence = tmpCameraDistance;

        shooter.unAim -= OnPlayerNotAim;
    }
    public void StopShake()
    {
        iTween.StopByName("shake");
        GetComponent<Animator>().enabled = false;
        
    }
}
