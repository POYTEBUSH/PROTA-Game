using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject StatPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetMenu()
    {
        MenuPanel.SetActive(true);
        StatPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void SetStat()
    {
        MenuPanel.SetActive(false);
        StatPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void SetSettings()
    {
        MenuPanel.SetActive(false);
        StatPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void SetCredits()
    {
        MenuPanel.SetActive(false);
        StatPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

}
