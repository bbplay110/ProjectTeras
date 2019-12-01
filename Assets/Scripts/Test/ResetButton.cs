using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetButton : MonoBehaviour {
    private string currentScene;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F12)){
            SceneManager.LoadScene(currentScene);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SceneManager.LoadScene("TestArea4");
        }
        if(Input.GetKeyDown(KeyCode.F10))
            SceneManager.LoadScene("LEVEL2_4finish");

        if (Input.GetKeyDown(KeyCode.F9))
            SceneManager.LoadScene("LEVEL2_3finish");
        
        if (Input.GetKeyDown(KeyCode.F8))
            SceneManager.LoadScene("LEVEL2_2-2");

        if (Input.GetKeyDown(KeyCode.F8))
            SceneManager.LoadScene("LEVEL2_2-1_finish");

        if (Input.GetKeyDown(KeyCode.F6))
            SceneManager.LoadScene("LEVEL2_1_finish");
        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene("LEVEL1_finish");
    }
}
