using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class optionMenu : MonoBehaviour {
    public AudioMixer Mixer;
    public AudioMixerSnapshot paused,unpaused;
    bool WaitForKey=false;
    private Button tmpButton;
    private string tmpInputName;
    private string tmpKey;
	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
        if (hInput.GetButtonDown("jump"))
        {
            Debug.Log("jump!");

        }
        else if (hInput.GetButtonDown("Submit"))
        {
            Debug.Log("Submit");
        }
        else if (hInput.GetButtonDown("Start"))
        {
            Debug.Log("Start");
        }

        if (WaitForKey && Input.anyKeyDown&&(!Input.GetMouseButton(0)&&!Input.GetMouseButton(1)&& !Input.GetMouseButton(2)))
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //your code here
                    tmpButton.GetComponentInChildren<Text>().text = vKey.ToString();
                    tmpButton.interactable = true;
                    WaitForKey = false;
                    setInput(vKey);
                }
            }
        }

	}
    public void SetVolumeChengeMaster(float mixlevel) {
        Mixer.SetFloat("Master",mixlevel);
        
        PlayerPrefs.SetFloat("MasterVolume", mixlevel);

    }
    public void SetVolumeChengeBGM(float mixlevel)
    {
        Mixer.SetFloat("Music", mixlevel);
        PlayerPrefs.SetFloat("BGMVolume", mixlevel);

    }
    public void SetVolumeChengeSFX(float mixlevel)
    {
        Mixer.SetFloat("SFX", mixlevel);
        PlayerPrefs.SetFloat("SFXVolume", mixlevel);
    }
    public void Exit(GameObject MenuToExit)
    {
        MenuToExit.SetActive(false);
    }
    public void Open(GameObject toOpen)
    {
        toOpen.SetActive(true);
    }
    public void ClickSetKeyButton(Button n)
    {
        string InputName = n.name;
        if (WaitForKey) {
            tmpButton.GetComponentInChildren<Text>().text = tmpKey;
            tmpButton.interactable = true;
            tmpKey = n.GetComponentInChildren<Text>().text;
            n.GetComponentInChildren<Text>().text = null;
            n.interactable = false;
            tmpButton = n;
            tmpInputName = InputName;
        }
        else
        {
            WaitForKey = true;
            tmpKey = n.GetComponentInChildren<Text>().text;
            n.GetComponentInChildren<Text>().text = null;
            n.interactable = false;
            tmpButton = n;
            
            tmpInputName = InputName;
        }


    }
    void setInput(KeyCode key)
    {
        hInput.SetKey(tmpInputName,key);
    }

}
