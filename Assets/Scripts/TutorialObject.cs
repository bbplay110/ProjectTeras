using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour {

    public GameObject Tip;
    public GameObject Panel;
    bool PanelOpening;



    void Start() {
        Tip.SetActive(false);
        Panel.SetActive(false);
        PanelOpening = false;
    }

    public void OpenTips()
    {
        Tip.SetActive(true);
        if (hInput.GetButtonDown("Submit"))
        {
            PanelManager();
        }

    }

    public void ExitTips()
    {
        Tip.SetActive(false);
    }

    public void PanelManager()
    {
        if (!PanelOpening)
        {
            OpenPanel();
        }
        else
        {
            closePane();
        }
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);
        PanelOpening = true;
    }

    public void closePane ()
    {
        Panel.SetActive(false);
        PanelOpening = false;
    }
	
	void Update () {
		
	}
}
