using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class bulletTime : MonoBehaviour {
    //delegate Void 委派函數 https://docs.microsoft.com/zh-tw/dotnet/api/system.delegate?view=netframework-4.8
    //還不能直接Call它
    public delegate void onPauseTime();
    public delegate void unPauseTime();
    //判斷時間是否是暫停的Bool
    private bool isPaused = false;
    //時間暫停跟解除時間暫停的Event,在其他腳本中向這事件註冊該腳本的函數
    public static event onPauseTime OnPauseTime;
    public static event onPauseTime UnPauseTime;
    //玩家的Component,不是玩家的GameObject
    private Player player;
    private PostProcessVolume cameraVolume;

    public  bool IsPaused
    {
        get
        {
            return isPaused;
        }

        set
        {
            isPaused = value;
        }
    }

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        cameraVolume = Camera.main.GetComponent<PostProcessVolume>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (hInput.GetButtonDown("Tab")&&PauseMenu.GamePause==false)
        {
            PauseStart();
        }*/
        checkEnergy();
	}
    void checkEnergy() {
        if (isPaused&&player.Energy>0)
        {
            player.Energy -= 1 * Time.deltaTime;
            //Debug.Log("checkEnergy="+player.Energy);
            if (player.Energy <= 0)
            {
                pauseEnd();
            }
        }
        if (!isPaused&& player.Energy<= player.maxEnergy)
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
