using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
    public SaveLoadUtility slu;
    public Scene loadScene;
	// Use this for initialization
	void Start () {
        if (slu == null)
        {
            slu = GameObject.Find("SaveManager").GetComponent<SaveLoadUtility>();
            if (slu == null)
            {
                Debug.Log("[SaveLoadMenu] Start(): Warning! SaveLoadUtility not assigned!");
            }
            //slu.LoadGame();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
