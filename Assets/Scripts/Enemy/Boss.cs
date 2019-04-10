using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public GameObject HpSet;
    public bool turnWithPlayer;
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
        PlayerCamera = GameObject.FindObjectOfType<Camera3rdControl>().target;
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
                Debug.Log("only_take_0_or_1_or2");
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
            PlayerCamera.position = ((Player.position + new Vector3(0, 2, 0)) + (transform.position + new Vector3(0, 50, 0))) / 2;
            FindObjectOfType<Camera3rdControl>().distence = Vector3.Distance(Player.position + new Vector3(0, 2, 0), transform.position + new Vector3(0, 50, 0)) + 8;

    }
    private void OnEnable()
    {
        HpSet.SetActive(true);
        tmpCameraPoint = PlayerCamera.localPosition;
        tmpCameraDistance = FindObjectOfType<Camera3rdControl>().distence;
        FindObjectOfType<Camera3rdControl>().maxDistence = 300;
        

        CanControllCamera = true;
    }
    private void OnDisable()
    {
        if (tmpCameraPoint != null)
            PlayerCamera.localPosition = tmpCameraPoint;
        FindObjectOfType<Camera3rdControl>().maxDistence = 8;
        FindObjectOfType<Camera3rdControl>().distence = tmpCameraDistance;

    }
}
