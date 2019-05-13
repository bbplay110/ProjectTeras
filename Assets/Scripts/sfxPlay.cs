using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class sfxPlay : MonoBehaviour {
    public AudioClip[] sfx;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();

            //audioSource.outputAudioMixerGroup=
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
	}
	public void playSfx(int sfxToPlay)
    {
        audioSource.PlayOneShot(sfx[sfxToPlay]);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
