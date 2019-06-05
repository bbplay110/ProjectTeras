using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTime : MonoBehaviour {
    public delegate void onPauseTime();
    public delegate void unPauseTime();
    public bool isPaused=false;
    public static event onPauseTime OnPauseTime;
    public static event onPauseTime UnPauseTime;
    private Player player;
    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
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
        isPaused = true;
        OnPauseTime();
    }
    public void pauseEnd()
    {
        isPaused = false;
        UnPauseTime();
    }
}
