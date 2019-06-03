using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityChenger : MonoBehaviour {
    public GameObject Menu;
    private GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");

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
}
