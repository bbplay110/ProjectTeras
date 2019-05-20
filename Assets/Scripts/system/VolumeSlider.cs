using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;



public class VolumeSlider : MonoBehaviour {
    public AudioMixer mixer;
    private float volume;
    private enum volumeMode
    {
        Main,
        BGM,
        SFX
    }
    [SerializeField]
    private volumeMode vvvolume;
	// Use this for initialization
    
	void Start () {
        mixer = (AudioMixer)Resources.Load("MasterMixer.mixer", typeof(AudioMixer));
        
        switch (vvvolume)
        {
            case volumeMode.Main:
                
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
                mixer.GetFloat("Master",out volume);
                break;
            case volumeMode.BGM:
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGMVolume");
                mixer.GetFloat("Music", out volume);
                break;
            case volumeMode.SFX:
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
                mixer.GetFloat("SFX", out volume);
                break;
        }
        gameObject.GetComponent<Slider>().value = volume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
