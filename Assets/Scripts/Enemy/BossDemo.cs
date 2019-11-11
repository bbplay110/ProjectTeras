using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDemo : MonoBehaviour {
    
    public bool turnWithPlayer;
    public bool look=false;

    private Animator BossAnimator;
    public float rightHandIKWeight=0;
    private Transform Player;
    public GameObject IKAim;
    private float lookWeight=0;
    //--時停變數
    private Animator Ani;

    //--
    // Use this for initialization
    void Start () {
        BossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (look)
        {
            if (lookWeight < 1)
            {
                lookWeight += 1*Time.deltaTime;
            }
        }
        if (!look && lookWeight > 0)
        {

            lookWeight -= 1 * Time.deltaTime;
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (look)
        {
            BossAnimator.SetLookAtWeight(lookWeight,lookWeight*0.5f,lookWeight,lookWeight);
            BossAnimator.SetLookAtPosition(Player.position+new Vector3(0,15,0));
        }
        else if (!look)
        {

            BossAnimator.SetLookAtWeight(lookWeight, lookWeight * 0.5f,lookWeight,lookWeight);
        }
    }
    public void loook()
    {
    }

}
