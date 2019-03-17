using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPortal : MonoBehaviour {
    
    private bool IsActive;
    public GameObject tips = null; 
	// Use this for initialization
	void Start () {

        hurt.BossDeath += ShowPortal;
    }
    private void OnDestroy()
    {
        hurt.BossDeath -= ShowPortal;
    }
    public void ShowPortal(int secand)
    {
        gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsActive = false;
        }
    }
    // Update is called once per frame
    void Update () {
        if (IsActive)
        {
            tips.SetActive(true);
            if (hInput.GetButtonDown("Submit"))
            {
                GetComponent<SceneLoad>().nextScenes();
            }
        }
        else
        {
            tips.SetActive(false);
        }
	}
}
