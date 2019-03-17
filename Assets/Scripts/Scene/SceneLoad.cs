using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(scenes);

        }
        else
        {
            Debug.LogWarning("nullScene");
        }
    }
    public void LoadScene(string Scene)
    {

        SceneManager.LoadScene(Scene);
    }
	public void quit(){
		Application.Quit ();
	}
}
