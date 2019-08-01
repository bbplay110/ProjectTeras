using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour {
    public static bool GamePause = false;
    public bool pauseEditor = false;
    public GameObject PauseMenuUI;
    // Use this for initialization

    public AudioMixerSnapshot paused, unpaused;
    void Start () {
        if (paused != null && unpaused != null)
        {
            unpaused.TransitionTo(1f);
        }
    }
    void Lowpass()
    {
        if(paused!=null&& unpaused != null) {
            if (Time.timeScale == 0)
            {
                paused.TransitionTo(1f);
            }
            else
            {
                unpaused.TransitionTo(1f);
            }
        }
        else
        {
            Debug.LogWarning("PauseMenu.paused or PauseMenu.unpaused is null!");
        }
    }

    // Update is called once per frame
    void Update () {
        if (hInput.GetButtonDown("Start")) {
            if (GamePause) {
                Resume();
                Lowpass();
            }
            else
            {
                Pause();
                Lowpass();
            }
        }
	}
    public void Pause() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
        if (pauseEditor)
        {
            Debug.Break();
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GamePause = false;
    }
    public void Option()
    {
        Debug.Log("Seting");
    }
    public void QuitGame() {
        Application.Quit();
    }
}
