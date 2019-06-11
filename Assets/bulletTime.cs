using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class bulletTime : MonoBehaviour {
    public delegate void onPauseTime();
    public delegate void unPauseTime();
    public bool isPaused=false;
    public static event onPauseTime OnPauseTime;
    public static event onPauseTime UnPauseTime;
    private Player player;
    private PostProcessVolume cameraVolume;
    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        cameraVolume = Camera.main.GetComponent<PostProcessVolume>();
	}
	
	// Update is called once per frame
	void Update () {
        
        checkEnergy();
	}
    void checkEnergy() {
        if (isPaused&&player.Energy>0)
        {
            player.Energy -= 1 * Time.deltaTime;
            Debug.Log("checkEnergy="+player.Energy);
            if (player.Energy <= 0)
            {
                pauseEnd();
            }
        }
        if (!isPaused)
        {
            player.Energy += 1 * Time.deltaTime;
        }

    }
    public void  PauseStart()
    {
        if (isPaused == false)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", 0.3f, "onupdate", "setCameraEffectVolume"));
            isPaused = true;
            OnPauseTime();
        }
        else
        {
            pauseEnd();
        }

    }
    public void pauseEnd()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.3f, "onupdate", "setCameraEffectVolume"));
        isPaused = false;
        UnPauseTime();
    }
    void setCameraEffectVolume(float newValue)
    {
        cameraVolume.weight = newValue;
    }
}
