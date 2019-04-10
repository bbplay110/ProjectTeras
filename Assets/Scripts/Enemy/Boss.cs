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
	// Use this for initialization
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
        

    }
    private void OnEnable()
    {
        HpSet.SetActive(true);
    }
}
