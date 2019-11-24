using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class testttt : MonoBehaviour
{

    public GameObject aaa;
    private void Start()
    {
        aaa.SetActive(false);
    }
    private void OnGUI()
    {
        if (aaa == null)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "aaa is nulls");

        }
       
    }
}