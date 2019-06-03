using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityChenger : MonoBehaviour {
    public GameObject Menu;
    private GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.1f;

        }
        if (Input.GetKey(KeyCode.Tab))
        {
            //Menu.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
	}
    public void ChangeWapon(float waponCode)
    {
        /*
         * waponCode:
         0:空手+手槍
         1:三叉戟+弩
         */
        //Player.GetComponent<Attacker>().enabled = false;
        Animator PlayerAnimator = Player.GetComponent<Animator>();
        PlayerAnimator.SetFloat("wapon",waponCode);
        //Player.GetComponent<Attacker>().enabled = true;

    }
}
