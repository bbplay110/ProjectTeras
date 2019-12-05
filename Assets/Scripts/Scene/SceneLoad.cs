using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoad : MonoBehaviour {
    public string scenes;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void nextScenes(){
        if (scenes != null)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(scenes);
        }
        else
        {
            Debug.LogWarning("nullScene");
        }
    }
    
    public void LoadScene(string Scene)
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(Scene);
    }
	public void quit(){
		Application.Quit ();
	}
}
